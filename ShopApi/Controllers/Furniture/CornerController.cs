using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DAL.Repositories.Furniture.Corner;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations;

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

        [HttpPost("{id}")]
        public async Task<ActionResult<CornerReadDto>> GetByIdAsync(int id)
        {
            var model = await _repository.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CornerReadDto>(model));
        }
    }
}