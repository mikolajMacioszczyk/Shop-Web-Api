using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApi.DAL.Repositories.Collection;

namespace ShopApi.QueryBuilder.Collection
{
    public class CollectionQueryBuilder : ICollectionQueryBuilder
    {
        private readonly ICollectionRepository _repository;
        private IQueryable<Models.Furnitures.Collection> _query;

        public CollectionQueryBuilder(ICollectionRepository repository)
        {
            _repository = repository;
        }

        public ICollectionQueryBuilder GetAll()
        {
            _query = _repository.GetIQuerable();
            return this;
        }

        public ICollectionQueryBuilder WithNameLike(string pattern)
        {
            _query = from c in _query
                where EF.Functions.Like(c.Name, pattern)
                select c;
            return this;
        }

        public ICollectionQueryBuilder OnlyNew()
        {
            _query = _query.Where(c => c.IsNew);
            return this;
        }

        public ICollectionQueryBuilder OnlyLimited()
        {
            _query = _query.Where(c => c.IsLimited);
            return this;
        }

        public ICollectionQueryBuilder OnlyOnSale()
        {
            _query = _query.Where(c => c.IsOnSale);
            return this;
        }

        public async Task<IEnumerable<Models.Furnitures.Collection>> ToListAsync()
        {
            var output = await _query.ToListAsync();
            _query = null;
            return output;
        }
    }
}