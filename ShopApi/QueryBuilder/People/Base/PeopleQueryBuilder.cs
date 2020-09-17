using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApi.DAL.Repositories.People.Base;
using ShopApi.Models.People;

namespace ShopApi.QueryBuilder.People.Base
{
    public class PeopleQueryBuilder : IPeopleQueryBuilder
    {
        private readonly IPeopleRepository _repository;
        private IQueryable<Person> _query;
        
        public PeopleQueryBuilder(IPeopleRepository repository)
        {
            _repository = repository;
        }

        public IPeopleQueryBuilder GetAll()
        {
            _query = _repository.GetQuerable();
            return this;
        }

        public IPeopleQueryBuilder WithNameLike(string pattern)
        {
            _query = from p in _query
                where EF.Functions.Like(p.Name, pattern)
                select p;
            return this;
        }

        public IPeopleQueryBuilder WithAddress(int addressId)
        {
            _query = _query.Where(p => p.Address.Id == addressId);
            return this;
        }

        public async Task<List<Person>> ToListAsync()
        {
            var output = await _query.ToListAsync();
            _query = null;
            return output;
        }
    }
}