using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi.DAL.Repositories.Address;
using ShopApi.Models.Dtos.Address;
using ShopApi.Models.People;
using ShopApi.QueryBuilder.Address;

namespace ShopApi.Controllers.Addresses
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : Controller
    {
        private readonly IAddressRepository _repository;
        private readonly IAddressQueryBuilder _queryBuilder;
        private readonly IMapper _mapper;

        public AddressController(IAddressRepository repository, IMapper mapper, IAddressQueryBuilder queryBuilder)
        {
            _repository = repository;
            _mapper = mapper;
            _queryBuilder = queryBuilder;
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
                var addressReadDto = _mapper.Map<AddressReadDto>(model);
                addressReadDto.Id = id;
                return Accepted(nameof(GetByIdAsync), addressReadDto);
            }
            return BadRequest("Invalid address id");
        }
        
        [HttpPost("create")]
        public async Task<ActionResult<AddressReadDto>> CreateAsync([FromBody] AddressCreateDto addressCreateDto)
        {
            var model = _mapper.Map<Address>(addressCreateDto);
            if (await _repository.CreateAsync(model))
            {
                await _repository.SaveChangesAsync();
                var addressReadDto = _mapper.Map<AddressReadDto>(model);
                return Created(nameof(CreateAsync), addressReadDto);
            }
            return BadRequest("Error when try to create address in database");
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                if (await _repository.RemoveAsync(id))
                {

                    await _repository.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }

            return NotFound("Not Found Address with given Id");
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Address>>> SearchAsync([FromBody] AddressSearchDto addressSearchDto)
        {
            _queryBuilder.GetAll();
            if (!string.IsNullOrEmpty(addressSearchDto.City))
                _queryBuilder.WithCityLike(addressSearchDto.City);
            if (!string.IsNullOrEmpty(addressSearchDto.Street))
                _queryBuilder.WithStreetLike(addressSearchDto.Street);
            if (!string.IsNullOrEmpty(addressSearchDto.PostalCode))
                _queryBuilder.WithPostalCode(addressSearchDto.PostalCode);
            if (addressSearchDto.House.HasValue)
                _queryBuilder.WithHouse(addressSearchDto.House.Value);

            return Ok(_mapper.Map<IEnumerable<AddressReadDto>>(await _queryBuilder.ToListAsync()));
        }
    }
}