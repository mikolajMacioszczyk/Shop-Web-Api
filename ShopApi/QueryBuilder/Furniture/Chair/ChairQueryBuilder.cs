using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApi.DAL.Repositories.Furniture.Chair;
using ShopApi.QueryBuilder.Furniture.Base;

namespace ShopApi.QueryBuilder.Furniture.Chair
{
    public class ChairQueryBuilder : IChairQueryBuilder
    {
        private readonly IChairRepository _repository;
        private IQueryable<Models.Furnitures.FurnitureImplmentation.Chair> _query;

        public ChairQueryBuilder(IChairRepository repository)
        {
            _repository = repository;
        }

        public IChairQueryBuilder GetAll()
        {
            _query = _repository.GetIQuerable();
            return this;
        }

        public IChairQueryBuilder WithNameLike(string pattern)
        {
            _query = from f in _query
                where EF.Functions.Like(f.Name, pattern)
                select f;
            return this;
        }

        public IChairQueryBuilder WithPrizeGreaterThan(double minValue)
        {
            _query = _query.Where(f => f.Prize >= minValue);
            return this;
        }

        public IChairQueryBuilder WithPrizeSmallerThan(double maxValue)
        {
            _query = _query.Where(f => f.Prize <= maxValue);
            return this;
        }

        public IChairQueryBuilder WithCollection(int collectionId)
        {
            _query = _query.Where(f => f.Collection.Id == collectionId);
            return this;
        }

        public IChairQueryBuilder WithWidthGraterThan(int minWidth)
        {
            _query = _query.Where(f => f.Width >= minWidth);
            return this;
        }

        public IChairQueryBuilder WithWidthSmallerThan(int maxWidth)
        {
            _query = _query.Where(f => f.Width <= maxWidth);
            return this;
        }

        public IChairQueryBuilder WithLengthGraterThan(int minLength)
        {
            _query = _query.Where(f => f.Length >= minLength);
            return this;
        }

        public IChairQueryBuilder WithLengthSmallerThan(int maxLength)
        {
            _query = _query.Where(f => f.Length <= maxLength);
            return this;
        }

        public IChairQueryBuilder WithHeightGraterThan(int minHeight)
        {
            _query = _query.Where(f => f.Height >= minHeight);
            return this;
        }

        public IChairQueryBuilder WithHeightSmallerThan(int maxHeight)
        {
            _query = _query.Where(f => f.Height <= maxHeight);
            return this;
        }

        public IChairQueryBuilder WithWeightGraterThan(int minWeight)
        {
            _query = _query.Where(f => f.Weight >= minWeight);
            return this;
        }

        public IChairQueryBuilder WithWeightSmallerThan(int maxWeight)
        {
            _query = _query.Where(f => f.Weight <= maxWeight);
            return this;
        }

        public async Task<List<Models.Furnitures.FurnitureImplmentation.Chair>> ToListAsync()
        {
            var output = await _query.ToListAsync();
            _query = null;
            return output;
        }
    }
}