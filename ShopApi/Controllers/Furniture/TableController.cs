using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DAL.Repositories.Furniture.Table;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Table;
using ShopApi.Models.Furnitures.FurnitureImplmentation;
using ShopApi.QueryBuilder.Furniture.Table;

namespace ShopApi.Controllers.Furniture
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : Controller
    {
        private readonly ITableRepository _repository;
        private readonly ITableQueryBuilder _queryBuilder;
        private readonly IMapper _mapper;

        public TableController(ITableRepository repository, IMapper mapper, ITableQueryBuilder queryBuilder)
        {
            _repository = repository;
            _mapper = mapper;
            _queryBuilder = queryBuilder;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TableReadDto>>> GetAllAsync()
        {
            return Ok(_mapper.Map<IEnumerable<TableReadDto>>(await _repository.GetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TableReadDto>> GetByIdAsync([FromRoute]int id)
        {
            var model = await _repository.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<TableReadDto>(model));
        }
        
        [HttpPut("update/{id}")]
        public async Task<ActionResult<TableReadDto>> UpdateAsync([FromRoute]int id,[FromBody] TableUpdateDto tableUpdateDto)
        {
            Table model = _mapper.Map<Table>(tableUpdateDto);
            if (await _repository.UpdateAsync(id,model))
            {
                await _repository.SaveChangesAsync();
                var tableReadDto = _mapper.Map<TableReadDto>(model);
                tableReadDto.Id = id;
                return Accepted(nameof(GetByIdAsync), tableReadDto);
            }
            return BadRequest("Invalid Table Id");
        }
        
        [HttpPost("create")]
        public async Task<ActionResult<TableReadDto>> CreateAsync([FromBody] TableCreateDto tableCreateDto)
        {
            var model = _mapper.Map<Table>(tableCreateDto);
            if (await _repository.CreateAsync(model))
            {
                await _repository.SaveChangesAsync();
                var tableReadDto = _mapper.Map<TableReadDto>(model);
                return Created(nameof(CreateAsync), tableReadDto);
            }
            return BadRequest("Error when try to create table in database");
        }
        
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            if (await _repository.RemoveAsync(id))
            {
                await _repository.SaveChangesAsync();
                return NoContent();
            }
            return NotFound("Not Found Table with given Id");
        }
        
        [HttpGet("search")]
        private async Task<ActionResult<IEnumerable<Table>>> SearchAsync([FromBody] TableSearchDto tableSearchDto)
        {
            _queryBuilder.GetAll();
            if (!string.IsNullOrEmpty(tableSearchDto.Name))
                _queryBuilder.WithNameLike(tableSearchDto.Name);
            if (tableSearchDto.MinPrize.HasValue)
                _queryBuilder.WithPrizeGreaterThan(tableSearchDto.MinPrize.Value);
            if (tableSearchDto.MaxPrize.HasValue)
                _queryBuilder.WithPrizeSmallerThan(tableSearchDto.MaxPrize.Value);
            if (tableSearchDto.MinHeight.HasValue)
                _queryBuilder.WithHeightGraterThan(tableSearchDto.MinHeight.Value);
            if (tableSearchDto.MaxHeight.HasValue)
                _queryBuilder.WithHeightSmallerThan(tableSearchDto.MaxHeight.Value);
            if (tableSearchDto.MinLength.HasValue)
                _queryBuilder.WithLengthGraterThan(tableSearchDto.MinLength.Value);
            if (tableSearchDto.MaxLength.HasValue)
                _queryBuilder.WithLengthSmallerThan(tableSearchDto.MaxLength.Value);
            if (tableSearchDto.MinWeight.HasValue)
                _queryBuilder.WithWeightGraterThan(tableSearchDto.MinWeight.Value);
            if (tableSearchDto.MaxWeight.HasValue)
                _queryBuilder.WithWeightSmallerThan(tableSearchDto.MaxWeight.Value);
            if (tableSearchDto.MinWidth.HasValue)
                _queryBuilder.WithWidthGraterThan(tableSearchDto.MinWidth.Value);
            if (tableSearchDto.MaxWidth.HasValue)
                _queryBuilder.WithWidthSmallerThan(tableSearchDto.MaxWidth.Value);
            if (tableSearchDto.CollectionId.HasValue)
                _queryBuilder.WithCollection(tableSearchDto.CollectionId.Value);
            if (!string.IsNullOrEmpty(tableSearchDto.Shape))
                _queryBuilder.WithShapeLike(tableSearchDto.Shape);
            if (tableSearchDto.IsFoldable.HasValue && tableSearchDto.IsFoldable.Value)
                _queryBuilder.OnlyFoldable();
            return Ok(_mapper.Map<IEnumerable<TableReadDto>>(await _queryBuilder.ToListAsync()));
        }
    }
}