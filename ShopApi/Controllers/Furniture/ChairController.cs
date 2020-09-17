using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DAL.Repositories.Furniture.Chair;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Chair;
using ShopApi.Models.Furnitures.FurnitureImplmentation;
using ShopApi.QueryBuilder.Furniture.Chair;

namespace ShopApi.Controllers.Furniture
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChairController : Controller
    {
        private readonly IChairRepository _repository;
        private readonly IChairQueryBuilder _queryBuilder;
        private readonly IMapper _mapper;

        public ChairController(IChairRepository repository, IMapper mapper, IChairQueryBuilder queryBuilder)
        {
            _repository = repository;
            _mapper = mapper;
            _queryBuilder = queryBuilder;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChairReadDto>>> GetAllAsync()
        {
            return Ok(_mapper.Map<IEnumerable<ChairReadDto>>(await _repository.GetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChairReadDto>> GetByIdAsync([FromRoute]int id)
        {
            var model = await _repository.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ChairReadDto>(model));
        }
        
        [HttpPut("update/{id}")]
        public async Task<ActionResult<ChairReadDto>> UpdateAsync([FromRoute]int id,[FromBody] ChairCreateDto chairCreateDto)
        {
            Chair model = _mapper.Map<Chair>(chairCreateDto);

            if (await _repository.UpdateAsync(id,model))
            {
                await _repository.SaveChangesAsync();
                var chairReadDto = _mapper.Map<ChairReadDto>(model);
                chairReadDto.Id = id;
                return Accepted(nameof(GetByIdAsync), chairReadDto);
            }
            return BadRequest("Invalid Chair Id");
        }
        
        [HttpPost("create")]
        public async Task<ActionResult<ChairReadDto>> CreateAsync([FromBody] ChairCreateDto chairCreateDto)
        {
            var model = _mapper.Map<Chair>(chairCreateDto);
            if (await _repository.CreateAsync(model))
            {
                await _repository.SaveChangesAsync();
                var chairReadDto = _mapper.Map<ChairReadDto>(model);
                return Created(nameof(CreateAsync), chairReadDto);
            }
            return BadRequest("Error when try to create chair in database");
        }
        
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            if (await _repository.RemoveAsync(id))
            {
                await _repository.SaveChangesAsync();
                return NoContent();
            }
            return NotFound("Not Found Chair with given Id");
        }

        [HttpGet("search")]
        private async Task<ActionResult<IEnumerable<Chair>>> SearchAsync([FromBody] ChairSearchDto chairSearchDto)
        {
            _queryBuilder.GetAll();
            if (!string.IsNullOrEmpty(chairSearchDto.Name))
                _queryBuilder.WithNameLike(chairSearchDto.Name);
            if (chairSearchDto.MinPrize.HasValue)
                _queryBuilder.WithPrizeGreaterThan(chairSearchDto.MinPrize.Value);
            if (chairSearchDto.MaxPrize.HasValue)
                _queryBuilder.WithPrizeSmallerThan(chairSearchDto.MaxPrize.Value);
            if (chairSearchDto.MinHeight.HasValue)
                _queryBuilder.WithHeightGraterThan(chairSearchDto.MinHeight.Value);
            if (chairSearchDto.MaxHeight.HasValue)
                _queryBuilder.WithHeightSmallerThan(chairSearchDto.MaxHeight.Value);
            if (chairSearchDto.MinLength.HasValue)
                _queryBuilder.WithLengthGraterThan(chairSearchDto.MinLength.Value);
            if (chairSearchDto.MaxLength.HasValue)
                _queryBuilder.WithLengthSmallerThan(chairSearchDto.MaxLength.Value);
            if (chairSearchDto.MinWeight.HasValue)
                _queryBuilder.WithWeightGraterThan(chairSearchDto.MinWeight.Value);
            if (chairSearchDto.MaxWeight.HasValue)
                _queryBuilder.WithWeightSmallerThan(chairSearchDto.MaxWeight.Value);
            if (chairSearchDto.MinWidth.HasValue)
                _queryBuilder.WithWidthGraterThan(chairSearchDto.MinWidth.Value);
            if (chairSearchDto.MaxWidth.HasValue)
                _queryBuilder.WithWidthSmallerThan(chairSearchDto.MaxWidth.Value);
            if (chairSearchDto.CollectionId.HasValue)
                _queryBuilder.WithCollection(chairSearchDto.CollectionId.Value);

            return Ok(_mapper.Map<IEnumerable<ChairReadDto>>(await _queryBuilder.ToListAsync()));
        }
    }
}