using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DAL.Repositories.Furniture.Chair;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations;

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

        [HttpPost("{id}")]
        public async Task<ActionResult<ChairReadDto>> GetByIdAsync(int id)
        {
            var model = await _repository.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ChairReadDto>(model));
        }
    }
}