using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DAL.Repositories.Orders;
using ShopApi.Models.Dtos.Orders.OrderDtos;
using ShopApi.Models.Orders;
using ShopApi.QueryBuilder.Order;

namespace ShopApi.Controllers.Orders
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _repository;
        private readonly IOrderQueryBuilder _queryBuilder;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository repository, IMapper mapper, IOrderQueryBuilder queryBuilder)
        {
            _repository = repository;
            _mapper = mapper;
            _queryBuilder = queryBuilder;
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
        public async Task<ActionResult<OrderCreateDto>> UpdateAsync([FromRoute]int id,[FromBody] OrderUpdateDto orderUpdateDto)
        {
            Order model = _mapper.Map<Order>(orderUpdateDto);

            if (await _repository.UpdateAsync(id,model))
            {
                await _repository.SaveChangesAsync();
                var orderReadDto = _mapper.Map<OrderReadDto>(model);
                orderReadDto.Id = id;
                return Accepted(nameof(GetByIdAsync), orderReadDto);
            }
            return BadRequest("Invalid Table Id");
        }
        
        [HttpPost("create")]
        public async Task<ActionResult<OrderReadDto>> CreateAsync([FromBody] OrderCreateDto orderCreateDto)
        {
            var model = _mapper.Map<Order>(orderCreateDto);
            if (await _repository.CreateAsync(model))
            {
                await _repository.SaveChangesAsync();
                var orderReadDto = _mapper.Map<OrderReadDto>(model);
                return Created(nameof(CreateAsync), orderReadDto);
            }
            return BadRequest("Error when try to create order in database");
        }
        
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            if (await _repository.RemoveAsync(id))
            {
                await _repository.SaveChangesAsync();
                return NoContent();
            }
            return NotFound("Not Found Order with given Id");
        }

        [HttpGet("search")]
        private async Task<ActionResult<IEnumerable<OrderReadDto>>> SearchAsync([FromBody] OrderSearchDto orderSearchDto)
        {
            _queryBuilder.GetAll();
            if (!string.IsNullOrEmpty(orderSearchDto.Status))
                _queryBuilder.WithStatus(orderSearchDto.Status);
            if (orderSearchDto.FurnitureIds != null && orderSearchDto.FurnitureIds.Any())
                _queryBuilder.WithFurniture(orderSearchDto.FurnitureIds);
            
            if (orderSearchDto.MinTotalPrize.HasValue)
                _queryBuilder.WithTotalPrizeGreaterThan(orderSearchDto.MinTotalPrize.Value);
            if (orderSearchDto.MaxTotalPrize.HasValue)
                _queryBuilder.WithTotalPrizeSmallerThan(orderSearchDto.MaxTotalPrize.Value);
            
            if (orderSearchDto.MinTotalWeight.HasValue)
                _queryBuilder.WithTotalWeightGreaterThan(orderSearchDto.MinTotalWeight.Value);
            if (orderSearchDto.MaxTotalWeight.HasValue)
                _queryBuilder.WithTotalWeightSmallerThan(orderSearchDto.MaxTotalWeight.Value);

            if (orderSearchDto.MinDateOfAdmission.HasValue)
                _queryBuilder.WithDateOfAdmissionGreaterThan(orderSearchDto.MinDateOfAdmission.Value);
            if (orderSearchDto.MaxDateOfAdmission.HasValue)
                _queryBuilder.WithDateOfAdmissionSmallerThan(orderSearchDto.MaxDateOfAdmission.Value);

            if (orderSearchDto.MinDateOfRealization.HasValue)
                _queryBuilder.WithDateOfRealizationGreaterThan(orderSearchDto.MinDateOfRealization.Value);
            if (orderSearchDto.MaxDateOfRealization.HasValue)
                _queryBuilder.WithDateOfRealizationSmallerThan(orderSearchDto.MaxDateOfRealization.Value);

            return Ok(_mapper.Map<IEnumerable<OrderReadDto>>(await _queryBuilder.ToListAsync()));
        }
    }
}