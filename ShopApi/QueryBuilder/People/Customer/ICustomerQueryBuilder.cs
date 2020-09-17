using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApi.QueryBuilder.People.Customer
{
    public interface ICustomerQueryBuilder
    {
        ICustomerQueryBuilder GetAll();
        ICustomerQueryBuilder WithNameLike(string pattern);
        ICustomerQueryBuilder WithAddress(int addressId);
        ICustomerQueryBuilder WithOrders(int[] orderIds);
        Task<List<Models.People.Customer>> ToListAsync();
    }
}