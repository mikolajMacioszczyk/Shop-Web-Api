using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApi.DAL;
using ShopApi.DAL.Repositories.Address;

namespace ShopApi.QueryBuilder.Address
{
    public class AddressQueryBuilder : IAddressQueryBuilder
    {
        private IAddressRepository _repository;
        private IQueryable<Models.People.Address> _query;

        public AddressQueryBuilder(IAddressRepository repository)
        {
            _repository = repository;
        }

        public IAddressQueryBuilder GetAll()
        {
            _query = _repository.GetIQuerable();
            return this;
        }

        public IAddressQueryBuilder WithCityLike(string pattern)
        {
            _query =  from a in _query
                where EF.Functions.Like(a.City, pattern)
                select a;
            return this;
        }

        public IAddressQueryBuilder WithStreetLike(string pattern)
        {
            _query =  from a in _query
                where EF.Functions.Like(a.Street, pattern)
                select a;
            return this;
        }

        public IAddressQueryBuilder WithHouse(int number)
        {
            _query = _query.Where(a => a.House == number);
            return this;
        }

        public IAddressQueryBuilder WithPostalCode(string postalCode)
        {
            _query = _query.Where(a => a.PostalCode.Equals(postalCode));
            return this;
        }

        public async Task<IEnumerable<Models.People.Address>> ToListAsync()
        {
            var output = await _query.ToListAsync();
            _query = null;
            return output;
        }
    }
}