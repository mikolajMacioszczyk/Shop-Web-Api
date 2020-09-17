using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DAL.Repositories.People.Emplyee;
using ShopApi.Models.Dtos.People.Employee;
using ShopApi.Models.People;
using ShopApi.QueryBuilder.People.Employee;

namespace ShopApi.Controllers.People
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _repository;
        private readonly IEmployeeQueryBuilder _queryBuilder;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository repository, IMapper mapper, IEmployeeQueryBuilder queryBuilder)
        {
            _repository = repository;
            _mapper = mapper;
            _queryBuilder = queryBuilder;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> GetAllAsync()
        {
            return Ok(_mapper.Map<IEnumerable<EmployeeReadDto>>(await _repository.GetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeReadDto>> GetByIdAsync([FromRoute]int id)
        {
            var model = await _repository.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<EmployeeReadDto>(model));
        }
        
        [HttpPut("update/{id}")]
        public async Task<ActionResult<EmployeeReadDto>> UpdateAsync(int id,[FromBody] EmployeeUpdateDto employeeUpdateDto)
        {
            var model = _mapper.Map<Employee>(employeeUpdateDto);
            if (await _repository.UpdateAsync(id, model))
            {
                await _repository.SaveChangesAsync();
                var employeeReadDto = _mapper.Map<EmployeeReadDto>(model);
                employeeReadDto.Id = id;
                return Accepted(nameof(GetByIdAsync), employeeReadDto);
            }
            return BadRequest("Invalid employee id");
        }
        
        [HttpPost("create")]
        public async Task<ActionResult<EmployeeReadDto>> CreateAsync([FromBody] EmployeeCreateDto employeeCreateDto)
        {
            var model = _mapper.Map<Employee>(employeeCreateDto);
            if (await _repository.CreateAsync(model))
            {
                await _repository.SaveChangesAsync();
                var employeeReadDto = _mapper.Map<EmployeeReadDto>(model);
                return Created(nameof(CreateAsync), employeeReadDto);
            }
            return BadRequest("Error when try to create employee in database");
        }
        
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            if (await _repository.RemoveAsync(id))
            {
                await _repository.SaveChangesAsync();
                return NoContent();
            }
            return NotFound("Not Found Employee with given Id");
        }
        
        [HttpGet("search")]
        private async Task<ActionResult<IEnumerable<EmployeeReadDto>>> SearchAsync(
            [FromBody] EmployeeSearchDto employeeSearchDto)
        {
            _queryBuilder.GetAll();
            if (!string.IsNullOrEmpty(employeeSearchDto.Name))
                _queryBuilder.WithNameLike(employeeSearchDto.Name);
            if (employeeSearchDto.AddressId.HasValue)
                _queryBuilder.WithAddress(employeeSearchDto.AddressId.Value);
            if (!string.IsNullOrEmpty(employeeSearchDto.Permission))
                _queryBuilder.WithPermission(employeeSearchDto.Permission);
            if (!string.IsNullOrEmpty(employeeSearchDto.JobTitles))
                _queryBuilder.WithJobTitle(employeeSearchDto.JobTitles);
            if (employeeSearchDto.MinSalary.HasValue)
                _queryBuilder.WithSalaryGreaterThan(employeeSearchDto.MinSalary.Value);
            if (employeeSearchDto.MaxSalary.HasValue)
                _queryBuilder.WithSalarySmallerThan(employeeSearchDto.MaxSalary.Value);
            if (employeeSearchDto.MinDateOfBirth.HasValue)
                _queryBuilder.WithDateOfBirthGreaterThan(employeeSearchDto.MinDateOfBirth.Value);
            if (employeeSearchDto.MaxDateOfBirth.HasValue)
                _queryBuilder.WithDateOfBirthSmallerThan(employeeSearchDto.MaxDateOfBirth.Value);
            if (employeeSearchDto.MinDateOfEmployment.HasValue)
                _queryBuilder.WithDateOfEmploymentGreaterThan(employeeSearchDto.MinDateOfEmployment.Value);
            if (employeeSearchDto.MaxDateOfEmployment.HasValue)
                _queryBuilder.WithDateOfEmploymentSmallerThan(employeeSearchDto.MaxDateOfEmployment.Value);
                
            return Ok(_mapper.Map<IEnumerable<EmployeeReadDto>>(await _queryBuilder.ToListAsync()));
        }
    }
}