using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DAL.Repositories.Furniture.Corner;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Corner;
using ShopApi.Models.Furnitures.FurnitureImplmentation;
using ShopApi.QueryBuilder.Furniture.Corner;

namespace ShopApi.Controllers.Furniture
{
    [ApiController]
    [Route("api/[controller]")]
    public class CornerController : Controller
    {
        private readonly ICornerRepository _repository;
        private readonly ICornerQueryBuilder _queryBuilder;
        private readonly IMapper _mapper;

        public CornerController(ICornerRepository repository, IMapper mapper, ICornerQueryBuilder queryBuilder)
        {
            _repository = repository;
            _mapper = mapper;
            _queryBuilder = queryBuilder;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CornerReadDto>>> GetAllAsync()
        {
            return Ok(_mapper.Map<IEnumerable<CornerReadDto>>(await _repository.GetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CornerReadDto>> GetByIdAsync([FromRoute]int id)
        {
            var model = await _repository.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CornerReadDto>(model));
        }
        
        [HttpPut("update/{id}")]
        public async Task<ActionResult<CornerReadDto>> UpdateAsync([FromRoute]int id,[FromBody] CornerUpdateDto cornerUpdateDto)
        {
            Corner model = _mapper.Map<Corner>(cornerUpdateDto);
            if (await _repository.UpdateAsync(id,model))
            {
                await _repository.SaveChangesAsync();
                var cornerReadDto = _mapper.Map<CornerReadDto>(model);
                cornerReadDto.Id = id;
                return Accepted(nameof(GetByIdAsync), cornerReadDto);
            }
            return BadRequest("Invalid Corner Id");
        }
        
        [HttpPost("create")]
        public async Task<ActionResult<CornerReadDto>> CreateAsync([FromBody] CornerCreateDto cornerCreateDto)
        {
            var model = _mapper.Map<Corner>(cornerCreateDto);
            if (await _repository.CreateAsync(model))
            {
                await _repository.SaveChangesAsync();
                var cornerReadDto = _mapper.Map<CornerReadDto>(model);
                return Created(nameof(CreateAsync), cornerReadDto);
            }
            return BadRequest("Error when try to create corner in database");
        }
        
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            if (await _repository.RemoveAsync(id))
            {
                await _repository.SaveChangesAsync();
                return NoContent();
            }
            return NotFound("Not Found Corner with given Id");
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Corner>>> SearchAsync([FromBody] CornerSearchDto cornerSearchDto)
        {
            _queryBuilder.GetAll();
            if (!string.IsNullOrEmpty(cornerSearchDto.Name))
                _queryBuilder.WithNameLike(cornerSearchDto.Name);
            if (cornerSearchDto.MinPrize.HasValue)
                _queryBuilder.WithPrizeGreaterThan(cornerSearchDto.MinPrize.Value);
            if (cornerSearchDto.MaxPrize.HasValue)
                _queryBuilder.WithPrizeSmallerThan(cornerSearchDto.MaxPrize.Value);
            if (cornerSearchDto.MinHeight.HasValue)
                _queryBuilder.WithHeightGraterThan(cornerSearchDto.MinHeight.Value);
            if (cornerSearchDto.MaxHeight.HasValue)
                _queryBuilder.WithHeightSmallerThan(cornerSearchDto.MaxHeight.Value);
            if (cornerSearchDto.MinLength.HasValue)
                _queryBuilder.WithLengthGraterThan(cornerSearchDto.MinLength.Value);
            if (cornerSearchDto.MaxLength.HasValue)
                _queryBuilder.WithLengthSmallerThan(cornerSearchDto.MaxLength.Value);
            if (cornerSearchDto.MinWeight.HasValue)                    
                _queryBuilder.WithWeightGraterThan(cornerSearchDto.MinWeight.Value);
            if (cornerSearchDto.MaxWeight.HasValue)
                _queryBuilder.WithWeightSmallerThan(cornerSearchDto.MaxWeight.Value);
            if (cornerSearchDto.MinWidth.HasValue)
                _queryBuilder.WithWidthGraterThan(cornerSearchDto.MinWidth.Value);
            if (cornerSearchDto.MaxWidth.HasValue)
                _queryBuilder.WithWidthSmallerThan(cornerSearchDto.MaxWidth.Value);
            if (cornerSearchDto.CollectionId.HasValue)
                _queryBuilder.WithCollection(cornerSearchDto.CollectionId.Value);
            if (cornerSearchDto.HaveHeadrests.HasValue && cornerSearchDto.HaveHeadrests.Value)
                _queryBuilder.OnlyWithHeadrests();
            if (cornerSearchDto.HaveSleepMode.HasValue && cornerSearchDto.HaveSleepMode.Value)
                _queryBuilder.OnlyWithSleepMode();
            return Ok(_mapper.Map<IEnumerable<CornerReadDto>>(await _queryBuilder.ToListAsync()));
        }
    }
}