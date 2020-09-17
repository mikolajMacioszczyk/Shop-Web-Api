﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DAL.Repositories.Address;
using ShopApi.Models.Dtos.Collection;
using ShopApi.Models.Dtos.People.Address;
using ShopApi.Models.People;

namespace ShopApi.Controllers.Addresses
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : Controller
    {
        private readonly IAddressRepository _repository;
        private readonly IMapper _mapper;

        public AddressController(IAddressRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressReadDto>>> GetAllAsync()
        {
            return Ok(_mapper.Map<IEnumerable<AddressReadDto>>(await _repository.GetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AddressReadDto>> GetByIdAsync([FromRoute]int id)
        {
            var model = await _repository.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AddressReadDto>(model));
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<AddressReadDto>> UpdateAsync([FromRoute]int id,[FromBody] AddressUpdateDto addressUpdateDto)
        {
            var model = _mapper.Map<Address>(addressUpdateDto);
            if (await _repository.UpdateAsync(id,model))
            {
                await _repository.SaveChangesAsync();
                var addressReadDto = _mapper.Map<AddressReadDto>(await _repository.GetByIdAsync(id));
                addressReadDto.Id = id;
                return Accepted(nameof(GetByIdAsync), addressReadDto);
            }
            return BadRequest();
        }
    }
}