using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DAL.Repositories.Furniture.Sofa;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Sofa;
using ShopApi.Models.Furnitures.FurnitureImplmentation;
using ShopApi.QueryBuilder.Furniture.Sofa;

namespace ShopApi.Controllers.Furniture
{
    [ApiController]
    [Route("api/[controller]")]
    public class SofaController : Controller
    {
        private readonly ISofaRepository _repository;
        private readonly ISofaQueryBuilder _queryBuilder;
        private readonly IMapper _mapper;

        public SofaController(ISofaRepository repository, IMapper mapper, ISofaQueryBuilder queryBuilder)
        {
            _repository = repository;
            _mapper = mapper;
            _queryBuilder = queryBuilder;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SofaReadDto>>> GetAllAsync()
        {
            return Ok(_mapper.Map<IEnumerable<SofaReadDto>>(await _repository.GetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SofaReadDto>> GetByIdAsync([FromRoute]int id)
        {
            var model = await _repository.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SofaReadDto>(model));
        }
        
        [HttpPut("update/{id}")]
        public async Task<ActionResult<SofaReadDto>> UpdateAsync([FromRoute]int id,[FromBody] SofaUpdateDto sofaUpdateDto)
        {
            Sofa model = _mapper.Map<Sofa>(sofaUpdateDto);
            if (await _repository.UpdateAsync(id,model))
            {
                await _repository.SaveChangesAsync();
                var sofaReadDto = _mapper.Map<SofaReadDto>(model);
                sofaReadDto.Id = id;
                return Accepted(nameof(GetByIdAsync), sofaReadDto);
            }
            return BadRequest("Invalid Sofa Id");
        }
        
        [HttpPost("create")]
        public async Task<ActionResult<SofaReadDto>> CreateAsync([FromBody] SofaCreateDto sofaCreateDto)
        {
            var model = _mapper.Map<Sofa>(sofaCreateDto);
            if (await _repository.CreateAsync(model))
            {
                await _repository.SaveChangesAsync();
                var sofaReadDto = _mapper.Map<SofaReadDto>(model);
                return Created(nameof(CreateAsync), sofaReadDto);
            }
            return BadRequest("Error when try to create sofa in database");
        }
        
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                if (await _repository.RemoveAsync(id))
                {
                    await _repository.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
            return NotFound("Not Found Sofa with given Id");
        }

        [HttpGet("search")]
        private async Task<ActionResult<IEnumerable<Sofa>>> SearchAsync([FromBody] SofaSearchDto sofaSearchDto)
        {
            _queryBuilder.GetAll();
            if (!string.IsNullOrEmpty(sofaSearchDto.Name))
                _queryBuilder.WithNameLike(sofaSearchDto.Name);
            if (sofaSearchDto.MinPrize.HasValue)
                _queryBuilder.WithPrizeGreaterThan(sofaSearchDto.MinPrize.Value);
            if (sofaSearchDto.MaxPrize.HasValue)
                _queryBuilder.WithPrizeSmallerThan(sofaSearchDto.MaxPrize.Value);
            if (sofaSearchDto.MinHeight.HasValue)
                _queryBuilder.WithHeightGraterThan(sofaSearchDto.MinHeight.Value);
            if (sofaSearchDto.MaxHeight.HasValue)
                _queryBuilder.WithHeightSmallerThan(sofaSearchDto.MaxHeight.Value);
            if (sofaSearchDto.MinLength.HasValue)
                _queryBuilder.WithLengthGraterThan(sofaSearchDto.MinLength.Value);
            if (sofaSearchDto.MaxLength.HasValue)
                _queryBuilder.WithLengthSmallerThan(sofaSearchDto.MaxLength.Value);
            if (sofaSearchDto.MinWeight.HasValue)
                _queryBuilder.WithWeightGraterThan(sofaSearchDto.MinWeight.Value);
            if (sofaSearchDto.MaxWeight.HasValue)
                _queryBuilder.WithWeightSmallerThan(sofaSearchDto.MaxWeight.Value);
            if (sofaSearchDto.MinWidth.HasValue)
                _queryBuilder.WithWidthGraterThan(sofaSearchDto.MinWidth.Value);
            if (sofaSearchDto.MaxWidth.HasValue)
                _queryBuilder.WithWidthSmallerThan(sofaSearchDto.MaxWidth.Value);
            if (sofaSearchDto.CollectionId.HasValue)
                _queryBuilder.WithCollection(sofaSearchDto.CollectionId.Value);
            if (sofaSearchDto.HasSleepMode.HasValue && sofaSearchDto.HasSleepMode.Value)
                _queryBuilder.OnlyWithSleepMode();
            if (sofaSearchDto.MinPillows.HasValue)
                _queryBuilder.WithPillowsGreaterThan(sofaSearchDto.MinPillows.Value);
            if (sofaSearchDto.MaxPillows.HasValue)
                _queryBuilder.WithPillowsSmallerThan(sofaSearchDto.MaxPillows.Value);
            return Ok(_mapper.Map<IEnumerable<SofaReadDto>>(await _queryBuilder.ToListAsync()));
        }
    }
}