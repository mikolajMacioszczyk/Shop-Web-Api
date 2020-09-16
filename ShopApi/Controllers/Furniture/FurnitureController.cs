using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DAL.Repositories.Furniture.Base;
using ShopApi.Models.Dtos.Furniture;

namespace ShopApi.Controllers.Furniture
{
    [Route("api/[controller]")]
    [ApiController]
    public class FurnitureController : Controller
    {
        private readonly IFurnitureRepository _furnitureRepository;
        private readonly IMapper _mapper;

        public FurnitureController(IMapper mapper, IFurnitureRepository furnitureRepository)
        {
            _mapper = mapper;
            _furnitureRepository = furnitureRepository;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FurnitureReadDto>>> GetAllAsync()
        {
            return Ok(_mapper.Map<IEnumerable<FurnitureReadDto>>(await _furnitureRepository.GetAllAsync()));
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<FurnitureReadDto>> GetByIdAsync(int id)
        {
            var model = await _furnitureRepository.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<FurnitureReadDto>(model));
        }
    }
}