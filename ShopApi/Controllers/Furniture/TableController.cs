using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DAL.Repositories.Furniture.Table;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Table;
using ShopApi.Models.Furnitures.FurnitureImplmentation;

namespace ShopApi.Controllers.Furniture
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : Controller
    {
        private readonly ITableRepository _repository;
        private readonly IMapper _mapper;

        public TableController(ITableRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TableReadDto>>> GetAllAsync()
        {
            return Ok(_mapper.Map<IEnumerable<TableReadDto>>(await _repository.GetAllAsync()));
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<TableReadDto>> GetByIdAsync(int id)
        {
            var model = await _repository.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<TableReadDto>(model));
        }
        
        [HttpPut("update/{id}")]
        public async Task<ActionResult<TableReadDto>> UpdateAsync(int id,[FromBody] TableCreateDto tableCreateDto)
        {
            Table model = _mapper.Map<Table>(tableCreateDto);
            if (await _repository.UpdateAsync(id,model))
            {
                await _repository.SaveChangesAsync();
                var collectionReadDto = _mapper.Map<TableReadDto>(await _repository.GetByIdAsync(id));
                return Created(nameof(GetByIdAsync), collectionReadDto);
            }
            return BadRequest("Invalid Table Id");
        }
    }
}