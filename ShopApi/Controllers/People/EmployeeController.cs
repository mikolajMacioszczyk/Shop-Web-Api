using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DAL.Repositories.People.Emplyee;
using ShopApi.Models.Dtos.People.Employee;
using ShopApi.Models.People;

namespace ShopApi.Controllers.People
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> GetAllAsync()
        {
            return Ok(_mapper.Map<IEnumerable<EmployeeReadDto>>(await _repository.GetAllAsync()));
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<EmployeeReadDto>> GetByIdAsync(int id)
        {
            var model = await _repository.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<EmployeeReadDto>(model));
        }
        
        [HttpPut("update/{id}")]
        public async Task<ActionResult<EmployeeReadDto>> UpdateAsync(int id,[FromBody] EmployeeCreateDto customerCreateDto)
        {
            var model = _mapper.Map<Employee>(customerCreateDto);
            if (await _repository.UpdateAsync(id, model))
            {
                await _repository.SaveChangesAsync();
                var employeeReadDto = _mapper.Map<EmployeeReadDto>(model);
                employeeReadDto.Id = id;
                return Created(nameof(GetByIdAsync), employeeReadDto);
            }
            return BadRequest("Invalid employee id");
        }
    }
}