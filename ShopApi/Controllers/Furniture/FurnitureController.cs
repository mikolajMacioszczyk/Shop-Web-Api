using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DAL.Repositories.Furniture.Base;
using ShopApi.Models.Dtos.Furniture.Base;
using ShopApi.QueryBuilder.Furniture;
using ShopApi.QueryBuilder.Furniture.Base;

namespace ShopApi.Controllers.Furniture
{
    [Route("api/[controller]")]
    [ApiController]
    public class FurnitureController : Controller
    {
        private readonly IFurnitureRepository _furnitureRepository;
        private readonly IFurnitureQueryBuilder _queryBuilder;
        private readonly IMapper _mapper;

        public FurnitureController(IMapper mapper, IFurnitureRepository furnitureRepository, IFurnitureQueryBuilder queryBuilder)
        {
            _mapper = mapper;
            _furnitureRepository = furnitureRepository;
            _queryBuilder = queryBuilder;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FurnitureReadDto>>> GetAllAsync()
        {
            return Ok(_mapper.Map<IEnumerable<FurnitureReadDto>>(await _furnitureRepository.GetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FurnitureReadDto>> GetByIdAsync([FromRoute]int id)
        {
            var model = await _furnitureRepository.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<FurnitureReadDto>(model));
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Models.Furnitures.Furniture>>> SearchASync([FromBody] FurnitureSearchDto furnitureSearchDto)
        {
            _queryBuilder.GetAll();
            if (!string.IsNullOrEmpty(furnitureSearchDto.Name))
                _queryBuilder.WithNameLike(furnitureSearchDto.Name);
            if (furnitureSearchDto.MinPrize.HasValue)
                _queryBuilder.WithPrizeGreaterThan(furnitureSearchDto.MinPrize.Value);
            if (furnitureSearchDto.MaxPrize.HasValue)
                _queryBuilder.WithPrizeSmallerThan(furnitureSearchDto.MaxPrize.Value);
            if (furnitureSearchDto.MinHeight.HasValue)
                _queryBuilder.WithHeightGraterThan(furnitureSearchDto.MinHeight.Value);
            if (furnitureSearchDto.MaxHeight.HasValue)
                _queryBuilder.WithHeightSmallerThan(furnitureSearchDto.MaxHeight.Value);
            if (furnitureSearchDto.MinLength.HasValue)
                _queryBuilder.WithLengthGraterThan(furnitureSearchDto.MinLength.Value);
            if (furnitureSearchDto.MaxLength.HasValue)
                _queryBuilder.WithLengthSmallerThan(furnitureSearchDto.MaxLength.Value);
            if (furnitureSearchDto.MinWeight.HasValue)
                _queryBuilder.WithWeightGraterThan(furnitureSearchDto.MinWeight.Value);
            if (furnitureSearchDto.MaxWeight.HasValue)
                _queryBuilder.WithWeightSmallerThan(furnitureSearchDto.MaxWeight.Value);
            if (furnitureSearchDto.MinWidth.HasValue)
                _queryBuilder.WithWidthGraterThan(furnitureSearchDto.MinWidth.Value);
            if (furnitureSearchDto.MaxWidth.HasValue)
                _queryBuilder.WithWidthSmallerThan(furnitureSearchDto.MaxWidth.Value);
            if (furnitureSearchDto.CollectionId.HasValue)
                _queryBuilder.WithCollection(furnitureSearchDto.CollectionId.Value);
            return Ok(_mapper.Map<IEnumerable<FurnitureReadDto>>(await _queryBuilder.ToListAsync()));
        }
    }
}