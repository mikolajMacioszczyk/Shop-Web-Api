﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DAL.Repositories.Furniture.Sofa;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Sofa;
using ShopApi.Models.Furnitures.FurnitureImplmentation;

namespace ShopApi.Controllers.Furniture
{
    [ApiController]
    [Route("api/[controller]")]
    public class SofaController : Controller
    {
        private readonly ISofaRepository _repository;
        private readonly IMapper _mapper;

        public SofaController(ISofaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SofaReadDto>>> GetAllAsync()
        {
            return Ok(_mapper.Map<IEnumerable<SofaReadDto>>(await _repository.GetAllAsync()));
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<SofaReadDto>> GetByIdAsync(int id)
        {
            var model = await _repository.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SofaReadDto>(model));
        }
        
        [HttpPut("update/{id}")]
        public async Task<ActionResult<SofaReadDto>> UpdateAsync(int id,[FromBody] SofaCreateDto sofaCreateDto)
        {
            Sofa model = _mapper.Map<Sofa>(sofaCreateDto);
            if (await _repository.UpdateAsync(id,model))
            {
                await _repository.SaveChangesAsync();
                var collectionReadDto = _mapper.Map<SofaReadDto>(await _repository.GetByIdAsync(id));
                return Created(nameof(GetByIdAsync), collectionReadDto);
            }
            return BadRequest("Invalid Sofa Id");
        }
    }
}