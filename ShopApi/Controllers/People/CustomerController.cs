using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DAL.Repositories.People.Customer;
using ShopApi.Models.Dtos.People.Customer;
using ShopApi.Models.People;
using ShopApi.QueryBuilder.People.Customer;

namespace ShopApi.Controllers.People
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _repository;
        private readonly ICustomerQueryBuilder _queryBuilder;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository repository, IMapper mapper, ICustomerQueryBuilder queryBuilder)
        {
            _repository = repository;
            _mapper = mapper;
            _queryBuilder = queryBuilder;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerReadDto>>> GetAllAsync()
        {
            return Ok(_mapper.Map<IEnumerable<CustomerReadDto>>(await _repository.GetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerReadDto>> GetByIdAsync([FromRoute]int id)
        {
            var model = await _repository.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CustomerReadDto>(model));
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<CustomerReadDto>> UpdateAsync([FromRoute]int id,[FromBody] CustomerUpdateDto customerUpdateDto)
        {
            var model = _mapper.Map<Customer>(customerUpdateDto);
            if (await _repository.UpdateAsync(id, model))
            {
                await _repository.SaveChangesAsync();
                var customerReadDto = _mapper.Map<CustomerReadDto>(model);
                customerReadDto.Id = id;
                return Accepted(nameof(GetByIdAsync), customerReadDto);
            }
            return BadRequest("Invalid customer id");
        }
        
        [HttpPost("create")]
        public async Task<ActionResult<CustomerReadDto>> CreateAsync([FromBody] CustomerCreateDto customerCreateDto)
        {
            var model = _mapper.Map<Customer>(customerCreateDto);
            if (await _repository.CreateAsync(model))
            {
                await _repository.SaveChangesAsync();
                var customerReadDto = _mapper.Map<CustomerReadDto>(model);
                return Created(nameof(CreateAsync), customerReadDto);
            }
            return BadRequest("Error when try to create customer in database");
        }
        
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            if (await _repository.RemoveAsync(id))
            {
                await _repository.SaveChangesAsync();
                return NoContent();
            }
            return NotFound("Not Found Customer with given Id");
        }

        [HttpGet("search")]
        private async Task<ActionResult<IEnumerable<CustomerReadDto>>> SearchAsync(
            [FromBody] CustomerSearchDto customerSearchDto)
        {
            _queryBuilder.GetAll();
            if (!string.IsNullOrEmpty(customerSearchDto.Name))
                _queryBuilder.WithNameLike(customerSearchDto.Name);
            if (customerSearchDto.AddressId.HasValue)
                _queryBuilder.WithAddress(customerSearchDto.AddressId.Value);
            if (customerSearchDto.OrderIds != null && customerSearchDto.OrderIds.Any())
                _queryBuilder.WithOrders(customerSearchDto.OrderIds);

            return Ok(_mapper.Map<IEnumerable<CustomerReadDto>>(await _queryBuilder.ToListAsync()));
        }
    }
}