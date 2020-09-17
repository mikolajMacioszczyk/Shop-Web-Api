using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApi.QueryBuilder.Address
{
    public interface IAddressQueryBuilder
    {
        IAddressQueryBuilder GetAll();
        IAddressQueryBuilder WithCityLike(string pattern);
        IAddressQueryBuilder WithStreetLike(string pattern);
        IAddressQueryBuilder WithHouse(int number);
        IAddressQueryBuilder WithPostalCode(string postalCode);
        Task<IEnumerable<Models.People.Address>> ToListAsync();
    }
}