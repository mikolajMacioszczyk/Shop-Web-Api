using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DAL.Repositories.Orders;
using ShopApi.Models.Dtos.Orders;
using ShopApi.Models.Orders;

namespace ShopApi.Controllers.Orders
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetAllAsync()
        {
            return Ok(_mapper.Map<IEnumerable<OrderReadDto>>(await _repository.GetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderReadDto>> GetByIdAsync([FromRoute]int id)
        {
            var model = await _repository.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<OrderReadDto>(model));
        }
        
        [HttpPut("update/{id}")]
        public async Task<ActionResult<OrderCreateDto>> UpdateAsync([FromRoute]int id,[FromBody] OrderCreateDto orderCreateDto)
        {
            Order model = _mapper.Map<Order>(orderCreateDto);

            if (await _repository.UpdateAsync(id,model))
            {
                await _repository.SaveChangesAsync();
                var orderReadDto = _mapper.Map<OrderReadDto>(await _repository.GetByIdAsync(id));
                orderReadDto.Id = id;
                return Accepted(nameof(GetByIdAsync), orderReadDto);
            }
            return BadRequest("Invalid Table Id");
        }
    }
}