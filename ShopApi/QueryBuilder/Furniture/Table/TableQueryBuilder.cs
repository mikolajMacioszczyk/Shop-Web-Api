using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApi.DAL.Repositories.Furniture.Table;

namespace ShopApi.QueryBuilder.Furniture.Table
{
    public class TableQueryBuilder : ITableQueryBuilder
    {
        private readonly ITableRepository _repository;
        private IQueryable<Models.Furnitures.FurnitureImplmentation.Table> _query;

        public TableQueryBuilder(ITableRepository repository)
        {
            _repository = repository;
        }

        public ITableQueryBuilder GetAll()
        {
            _query = _repository.GetIQuerable();
            return this;
        }

        public ITableQueryBuilder WithNameLike(string pattern)
        {
            _query = from f in _query
                where EF.Functions.Like(f.Name, pattern)
                select f;
            return this;
        }

        public ITableQueryBuilder WithPrizeGreaterThan(double minValue)
        {
            _query = _query.Where(f => f.Prize >= minValue);
            return this;
        }

        public ITableQueryBuilder WithPrizeSmallerThan(double maxValue)
        {
            _query = _query.Where(f => f.Prize <= maxValue);
            return this;
        }

        public ITableQueryBuilder WithCollection(int collectionId)
        {
            _query = _query.Where(f => f.Collection.Id == collectionId);
            return this;
        }

        public ITableQueryBuilder WithWidthGraterThan(int minWidth)
        {
            _query = _query.Where(f => f.Width >= minWidth);
            return this;
        }

        public ITableQueryBuilder WithWidthSmallerThan(int maxWidth)
        {
            _query = _query.Where(f => f.Width <= maxWidth);
            return this;
        }

        public ITableQueryBuilder WithLengthGraterThan(int minLength)
        {
            _query = _query.Where(f => f.Length >= minLength);
            return this;
        }

        public ITableQueryBuilder WithLengthSmallerThan(int maxLength)
        {
            _query = _query.Where(f => f.Length <= maxLength);
            return this;
        }

        public ITableQueryBuilder WithHeightGraterThan(int minHeight)
        {
            _query = _query.Where(f => f.Height >= minHeight);
            return this;
        }

        public ITableQueryBuilder WithHeightSmallerThan(int maxHeight)
        {
            _query = _query.Where(f => f.Height <= maxHeight);
            return this;
        }

        public ITableQueryBuilder WithWeightGraterThan(int minWeight)
        {
            _query = _query.Where(f => f.Weight >= minWeight);
            return this;
        }

        public ITableQueryBuilder WithWeightSmallerThan(int maxWeight)
        {
            _query = _query.Where(f => f.Weight <= maxWeight);
            return this;
        }

        public ITableQueryBuilder OnlyFoldable()
        {
            _query = _query.Where(t => t.IsFoldable);
            return this;
        }

        public ITableQueryBuilder WithShapeLike(string pattern)
        {
            _query = from t in _query
                where EF.Functions.Like(t.Shape, pattern)
                select t;
            return this;
        }

        public async Task<List<Models.Furnitures.FurnitureImplmentation.Table>> ToListAsync()
        {
            var output = await _query.ToListAsync();
            _query = null;
            return output;
        }
    }
}