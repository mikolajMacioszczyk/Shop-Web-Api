using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DAL.Repositories.People.Base;
using ShopApi.Models.Dtos.People.Base;
using ShopApi.Models.Dtos.People.Employee;
using ShopApi.QueryBuilder.People;
using ShopApi.QueryBuilder.People.Base;

namespace ShopApi.Controllers.People
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : Controller
    {
        private readonly IPeopleRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPeopleQueryBuilder _queryBuilder;
        
        public PeopleController(IPeopleRepository repository, IMapper mapper, IPeopleQueryBuilder queryBuilder)
        {
            _repository = repository;
            _mapper = mapper;
            _queryBuilder = queryBuilder;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonReadDto>>> GetAllAsync()
        {
            return Ok(_mapper.Map<IEnumerable<PersonReadDto>>(await _repository.GetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeReadDto>> GetByIdAsync([FromRoute]int id)
        {
            var model = await _repository.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PersonReadDto>(model));
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<PersonReadDto>>> SearchAsync([FromBody] PersonSearchDto personSearchDto)
        {
            _queryBuilder.GetAll();
            if (!string.IsNullOrEmpty(personSearchDto.Name))
                _queryBuilder.WithNameLike(personSearchDto.Name);
            if (personSearchDto.AddressId.HasValue)
                _queryBuilder.WithAddress(personSearchDto.AddressId.Value);
            return Ok( _mapper.Map<IEnumerable<PersonReadDto>>(await _queryBuilder.ToListAsync()));
        }
        
    }
}