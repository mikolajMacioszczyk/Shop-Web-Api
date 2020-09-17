using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApi.DAL.Repositories.Furniture.Sofa;

namespace ShopApi.QueryBuilder.Furniture.Sofa
{
    public class SofaQueryBuilder : ISofaQueryBuilder
    {
        private readonly ISofaRepository _repository;
        private IQueryable<Models.Furnitures.FurnitureImplmentation.Sofa> _query;

        public SofaQueryBuilder(ISofaRepository repository)
        {
            _repository = repository;
        }

        public ISofaQueryBuilder GetAll()
        {
            _query = _repository.GetIQuerable();
            return this;
        }

        public ISofaQueryBuilder WithNameLike(string pattern)
        {
            _query = from f in _query
                where EF.Functions.Like(f.Name, pattern)
                select f;
            return this;
        }

        public ISofaQueryBuilder WithPrizeGreaterThan(double minValue)
        {
            _query = _query.Where(f => f.Prize >= minValue);
            return this;
        }

        public ISofaQueryBuilder WithPrizeSmallerThan(double maxValue)
        {
            _query = _query.Where(f => f.Prize <= maxValue);
            return this;
        }

        public ISofaQueryBuilder WithCollection(int collectionId)
        {
            _query = _query.Where(f => f.Collection.Id == collectionId);
            return this;
        }

        public ISofaQueryBuilder WithWidthGraterThan(int minWidth)
        {
            _query = _query.Where(f => f.Width >= minWidth);
            return this;
        }

        public ISofaQueryBuilder WithWidthSmallerThan(int maxWidth)
        {
            _query = _query.Where(f => f.Width <= maxWidth);
            return this;
        }

        public ISofaQueryBuilder WithLengthGraterThan(int minLength)
        {
            _query = _query.Where(f => f.Length >= minLength);
            return this;
        }

        public ISofaQueryBuilder WithLengthSmallerThan(int maxLength)
        {
            _query = _query.Where(f => f.Length <= maxLength);
            return this;
        }

        public ISofaQueryBuilder WithHeightGraterThan(int minHeight)
        {
            _query = _query.Where(f => f.Height >= minHeight);
            return this;
        }

        public ISofaQueryBuilder WithHeightSmallerThan(int maxHeight)
        {
            _query = _query.Where(f => f.Height <= maxHeight);
            return this;
        }

        public ISofaQueryBuilder WithWeightGraterThan(int minWeight)
        {
            _query = _query.Where(f => f.Weight >= minWeight);
            return this;
        }

        public ISofaQueryBuilder WithWeightSmallerThan(int maxWeight)
        {
            _query = _query.Where(f => f.Weight <= maxWeight);
            return this;
        }

        public ISofaQueryBuilder OnlyWithSleepMode()
        {
            _query = _query.Where(s => s.HasSleepMode);
            return this;
        }

        public ISofaQueryBuilder WithPillowsGreaterThan(int minPillows)
        {
            _query = _query.Where(s => s.Pillows >= minPillows);
            return this;
        }

        public ISofaQueryBuilder WithPillowsSmallerThan(int maxPillows)
        {
            _query = _query.Where(s => s.Pillows <= maxPillows);
            return this;
        }

        public async Task<List<Models.Furnitures.FurnitureImplmentation.Sofa>> ToListAsync()
        {
            var output = await _query.ToListAsync();
            _query = null;
            return output;
        }
    }
}