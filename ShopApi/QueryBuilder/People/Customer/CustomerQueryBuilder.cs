using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApi.DAL.Repositories.People.Customer;

namespace ShopApi.QueryBuilder.People.Customer
{
    public class CustomerQueryBuilder : ICustomerQueryBuilder
    {
        private readonly ICustomerRepository _repository;
        private IQueryable<Models.People.Customer> _query;

        public CustomerQueryBuilder(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public ICustomerQueryBuilder GetAll()
        {
            _query = _repository.GetIQuerable();
            return this;
        }

        public ICustomerQueryBuilder WithNameLike(string pattern)
        {
            _query = from p in _query
                where EF.Functions.Like(p.Name, pattern)
                select p;
            return this;
        }

        public ICustomerQueryBuilder WithAddress(int addressId)
        {
            _query = _query.Where(p => p.Address.Id == addressId);
            return this;
        }

        public ICustomerQueryBuilder WithOrders(int[] orderIds)
        {
            _query = _query.Where(c => orderIds.Contains(c.Id));
            return this;
        }

        public async Task<List<Models.People.Customer>> ToListAsync()
        {
            var output = await _query.ToListAsync();
            _query = null;
            return output;
        }
    }
}