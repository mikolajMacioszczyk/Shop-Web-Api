using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DAL.Repositories.Orders;
using ShopApi.DAL.Repositories.People.Customer;
using ShopApi.Models.Dtos.People;
using ShopApi.Models.Dtos.People.Customer;
using ShopApi.Models.Orders;
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

        [HttpPost("{id}")]
        public async Task<ActionResult<CustomerReadDto>> GetByIdAsync(int id)
        {
            var model = await _repository.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CustomerReadDto>(model));
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<CustomerReadDto>> UpdateAsync(int id,[FromBody] CustomerCreateDto customerCreateDto)
        {
            var model = _mapper.Map<Customer>(customerCreateDto);
            if (await _repository.UpdateAsync(id, model))
            {
                await _repository.SaveChangesAsync();
                var customerReadDto = _mapper.Map<CustomerReadDto>(model);
                customerReadDto.Id = id;
                return Created(nameof(GetByIdAsync), customerReadDto);
            }
            return BadRequest("Invalid customer id");
        }
    }
}