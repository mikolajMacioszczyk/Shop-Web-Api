using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DAL.Repositories.Furniture.Corner;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Corner;
using ShopApi.Models.Furnitures.FurnitureImplmentation;

namespace ShopApi.Controllers.Furniture
{
    [ApiController]
    [Route("api/[controller]")]
    public class CornerController : Controller
    {
        private readonly ICornerRepository _repository;
        private readonly IMapper _mapper;

        public CornerController(ICornerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
    }
}