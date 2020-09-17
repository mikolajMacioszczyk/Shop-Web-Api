using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApi.QueryBuilder.Collection
{
    public interface ICollectionQueryBuilder
    {
        ICollectionQueryBuilder GetAll();
        ICollectionQueryBuilder WithNameLike(string pattern);
        ICollectionQueryBuilder OnlyNew();
        ICollectionQueryBuilder OnlyLimited();
        ICollectionQueryBuilder OnlyOnSale();
        Task<IEnumerable<Models.Furnitures.Collection>> ToListAsync();
    }
}