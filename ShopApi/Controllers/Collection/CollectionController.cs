using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DAL.Repositories.Collection;
using ShopApi.Models.Dtos.Collection;
using ShopApi.QueryBuilder.Collection;

namespace ShopApi.Controllers.Collection
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : Controller
    {
        private readonly ICollectionRepository _repository;
        private readonly ICollectionQueryBuilder _queryBuilder;
        private readonly IMapper _mapper;

        public CollectionController(ICollectionRepository repository, IMapper mapper, ICollectionQueryBuilder queryBuilder)
        {
            _repository = repository;
            _mapper = mapper;
            _queryBuilder = queryBuilder;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollectionReadDto>>> GetAllAsync()
        {
            return Ok(_mapper.Map<IEnumerable<CollectionReadDto>>(await _repository.GetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CollectionReadDto>> GetByIdAsync([FromRoute]int id)
        {
            var model = await _repository.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CollectionReadDto>(model));
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<CollectionReadDto>> UpdateAsync([FromRoute]int id,[FromBody] CollectionUpdateDto collectionUpdateDto)
        {
            var model = _mapper.Map<Models.Furnitures.Collection>(collectionUpdateDto);
            if (await _repository.UpdateAsync(id,model))
            {
                await _repository.SaveChangesAsync();
                var collectionReadDto = _mapper.Map<CollectionReadDto>(model);
                collectionReadDto.Id = id;
                return Accepted(nameof(GetByIdAsync), collectionReadDto);
            }
            return BadRequest();
        }

        [HttpPost("create")]
        public async Task<ActionResult<CollectionReadDto>> CreateAsync([FromBody] CollectionCreateDto collectionCreateDto)
        {
            var model = _mapper.Map<Models.Furnitures.Collection>(collectionCreateDto);
            if (await _repository.CreateAsync(model))
            {
                await _repository.SaveChangesAsync();
                var collectionReadDto = _mapper.Map<CollectionReadDto>(model);
                return Created(nameof(CreateAsync), collectionReadDto);
            }
            return BadRequest("Error when try to create collection in database");
        }
        
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            if (await _repository.RemoveAsync(id))
            {
                await _repository.SaveChangesAsync();
                return NoContent();
            }
            return NotFound("Not Found Collection with given Id");
        }
        
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Models.Furnitures.Collection>>> SearchAsync([FromBody] CollectionSearchDto collectionSearchDto)
        {
            _queryBuilder.GetAll();
            if (!string.IsNullOrEmpty(collectionSearchDto.Name))
                _queryBuilder.WithNameLike(collectionSearchDto.Name);
            if (collectionSearchDto.IsLimited.HasValue && collectionSearchDto.IsLimited.Value)
                _queryBuilder.OnlyLimited();
            if (collectionSearchDto.IsNew.HasValue && collectionSearchDto.IsNew.Value)
                _queryBuilder.OnlyNew();
            if (collectionSearchDto.IsOnSale.HasValue && collectionSearchDto.IsOnSale.Value)
                _queryBuilder.OnlyOnSale();
            
            return Ok(_mapper.Map<IEnumerable<CollectionReadDto>>(await _queryBuilder.ToListAsync()));
        }
    }
}