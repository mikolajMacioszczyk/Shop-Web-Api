using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DAL.Repositories.Furniture.Chair;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Chair;
using ShopApi.Models.Furnitures.FurnitureImplmentation;

namespace ShopApi.Controllers.Furniture
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChairController : Controller
    {
        private readonly IChairRepository _repository;
        private readonly IMapper _mapper;

        public ChairController(IChairRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
                var chairReadDto = _mapper.Map<ChairReadDto>(await _repository.GetByIdAsync(id));
                chairReadDto.Id = id;
                return Accepted(nameof(GetByIdAsync), chairReadDto);
            }
            return BadRequest("Invalid Chair Id");
        }
    }
}