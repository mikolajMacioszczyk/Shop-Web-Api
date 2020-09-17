using System.Collections.Generic;
using System.Threading.Tasks;
using ShopApi.Models.People;

namespace ShopApi.QueryBuilder.People.Base
{
    public interface IPeopleQueryBuilder
    {
        IPeopleQueryBuilder GetAll();
        IPeopleQueryBuilder WithNameLike(string pattern);
        IPeopleQueryBuilder WithAddress(int addressId);
        Task<List<Person>> ToListAsync();
    }
}