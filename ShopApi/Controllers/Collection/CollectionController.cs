using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DAL.Repositories.Collection;
using ShopApi.Models.Dtos.Collection;

namespace ShopApi.Controllers.Collection
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : Controller
    {
        private readonly ICollectionRepository _repository;
        private readonly IMapper _mapper;

        public CollectionController(ICollectionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollectionReadDto>>> GetAllAsync()
        {
            return Ok(_mapper.Map<IEnumerable<CollectionReadDto>>(await _repository.GetAllAsync()));
        }

        [HttpPost("{id}", Name = "GetByIdAsync")]
        public async Task<ActionResult<CollectionReadDto>> GetByIdAsync(int id)
        {
            var model = await _repository.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CollectionReadDto>(model));
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<CollectionReadDto>> CreateAsync(int id,[FromBody] CollectionCreateDto collectionCreateDto)
        {
            var model = _mapper.Map<Models.Furnitures.Collection>(collectionCreateDto);
            if (await _repository.UpdateAsync(id,model))
            {
                await _repository.SaveChangesAsync();
                var collectionReadDto = _mapper.Map<CollectionReadDto>(await _repository.GetByIdAsync(id));
                return Created(nameof(GetByIdAsync), collectionReadDto);
            }
            return BadRequest();
        }
    }
}