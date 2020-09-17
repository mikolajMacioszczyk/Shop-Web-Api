using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApi.DAL.Repositories.Furniture.Corner;

namespace ShopApi.QueryBuilder.Furniture.Corner
{
    public class CornerQueryBuilder : ICornerQueryBuilder
    {
        private readonly ICornerRepository _repository;
        private IQueryable<Models.Furnitures.FurnitureImplmentation.Corner> _query;
        
        public CornerQueryBuilder(ICornerRepository repository)
        {
            _repository = repository;
        }

        public ICornerQueryBuilder GetAll()
        {
            _query = _repository.GetIQuerable();
            return this;
        }

        public ICornerQueryBuilder WithNameLike(string pattern)
        {
            _query = from f in _query
                where EF.Functions.Like(f.Name, pattern)
                select f;
            return this;
        }

        public ICornerQueryBuilder WithPrizeGreaterThan(double minValue)
        {
            _query = _query.Where(f => f.Prize >= minValue);
            return this;
        }

        public ICornerQueryBuilder WithPrizeSmallerThan(double maxValue)
        {
            _query = _query.Where(f => f.Prize <= maxValue);
            return this;
        }

        public ICornerQueryBuilder WithCollection(int collectionId)
        {
            _query = _query.Where(f => f.Collection.Id == collectionId);
            return this;
        }

        public ICornerQueryBuilder WithWidthGraterThan(int minWidth)
        {
            _query = _query.Where(f => f.Width >= minWidth);
            return this;
        }

        public ICornerQueryBuilder WithWidthSmallerThan(int maxWidth)
        {
            _query = _query.Where(f => f.Width <= maxWidth);
            return this;
        }

        public ICornerQueryBuilder WithLengthGraterThan(int minLength)
        {
            _query = _query.Where(f => f.Length >= minLength);
            return this;
        }

        public ICornerQueryBuilder WithLengthSmallerThan(int maxLength)
        {
            _query = _query.Where(f => f.Length <= maxLength);
            return this;
        }

        public ICornerQueryBuilder WithHeightGraterThan(int minHeight)
        {
            _query = _query.Where(f => f.Height >= minHeight);
            return this;
        }

        public ICornerQueryBuilder WithHeightSmallerThan(int maxHeight)
        {
            _query = _query.Where(f => f.Height <= maxHeight);
            return this;
        }

        public ICornerQueryBuilder WithWeightGraterThan(int minWeight)
        {
            _query = _query.Where(f => f.Weight >= minWeight);
            return this;
        }

        public ICornerQueryBuilder WithWeightSmallerThan(int maxWeight)
        {
            _query = _query.Where(f => f.Weight <= maxWeight);
            return this;
        }

        public ICornerQueryBuilder OnlyWithSleepMode()
        {
            _query = _query.Where(c => c.HaveSleepMode);
            return this;
        }

        public ICornerQueryBuilder OnlyWithHeadrests()
        {
            _query = _query.Where(c => c.HaveHeadrests);
            return this;
        }
        
        public async Task<List<Models.Furnitures.FurnitureImplmentation.Corner>> ToListAsync()
        {
            var output = await _query.ToListAsync();
            _query = null;
            return output;
        }
    }
}