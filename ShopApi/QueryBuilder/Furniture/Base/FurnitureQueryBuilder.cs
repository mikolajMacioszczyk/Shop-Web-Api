using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApi.DAL.Repositories.Furniture.Base;

namespace ShopApi.QueryBuilder.Furniture.Base
{
    public class FurnitureQueryBuilder : IFurnitureQueryBuilder
    {
        private readonly IFurnitureRepository _repository;
        private IQueryable<Models.Furnitures.Furniture> _query;

        public FurnitureQueryBuilder(IFurnitureRepository repository)
        {
            _repository = repository;
        }

        public IFurnitureQueryBuilder GetAll()
        {
            _query = _repository.GetQuerable();
            return this;
        }

        public IFurnitureQueryBuilder WithNameLike(string pattern)
        {
            _query = from f in _query
                where EF.Functions.Like(f.Name, pattern)
                select f;
            return this;
        }

        public IFurnitureQueryBuilder WithPrizeGreaterThan(double minValue)
        {
            _query = _query.Where(f => f.Prize >= minValue);
            return this;
        }

        public IFurnitureQueryBuilder WithPrizeSmallerThan(double maxValue)
        {
            _query = _query.Where(f => f.Prize <= maxValue);
            return this;
        }

        public IFurnitureQueryBuilder WithCollection(int collectionId)
        {
            _query = _query.Where(f => f.Collection.Id == collectionId);
            return this;
        }

        public IFurnitureQueryBuilder WithWidthGraterThan(int minWidth)
        {
            _query = _query.Where(f => f.Width >= minWidth);
            return this;
        }

        public IFurnitureQueryBuilder WithWidthSmallerThan(int maxWidth)
        {
            _query = _query.Where(f => f.Width <= maxWidth);
            return this;
        }

        public IFurnitureQueryBuilder WithLengthGraterThan(int minLength)
        {
            _query = _query.Where(f => f.Length >= minLength);
            return this;
        }

        public IFurnitureQueryBuilder WithLengthSmallerThan(int maxLength)
        {
            _query = _query.Where(f => f.Length <= maxLength);
            return this;
        }

        public IFurnitureQueryBuilder WithHeightGraterThan(int minHeight)
        {
            _query = _query.Where(f => f.Height >= minHeight);
            return this;
        }

        public IFurnitureQueryBuilder WithHeightSmallerThan(int maxHeight)
        {
            _query = _query.Where(f => f.Height <= maxHeight);
            return this;
        }

        public IFurnitureQueryBuilder WithWeightGraterThan(int minWeight)
        {
            _query = _query.Where(f => f.Weight >= minWeight);
            return this;
        }

        public IFurnitureQueryBuilder WithWeightSmallerThan(int maxWeight)
        {
            _query = _query.Where(f => f.Weight <= maxWeight);
            return this;
        }

        public async Task<List<Models.Furnitures.Furniture>> ToListAsync()
        {
            var output = await _query.ToListAsync();
            _query = null;
            return output;
        }
    }
}