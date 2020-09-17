using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DAL.Repositories.Orders;
using ShopApi.DAL.Repositories.People.Customer;
using ShopApi.Models.Dtos.People.Customer;
using ShopApi.Models.People;

namespace ShopApi.Controllers.People
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _repository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository repository, IMapper mapper, IOrderRepository orderRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _orderRepository = orderRepository;
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
    }
}