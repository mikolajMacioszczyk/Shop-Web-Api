using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApi.QueryBuilder.Furniture.Corner
{
    public interface ICornerQueryBuilder
    {
        ICornerQueryBuilder GetAll();
        ICornerQueryBuilder WithNameLike(string pattern);
        ICornerQueryBuilder WithPrizeGreaterThan(double minValue);
        ICornerQueryBuilder WithPrizeSmallerThan(double maxValue);
        ICornerQueryBuilder WithCollection(int collectionId);
        ICornerQueryBuilder WithWidthGraterThan(int minWidth);
        ICornerQueryBuilder WithWidthSmallerThan(int maxWidth);
        ICornerQueryBuilder WithLengthGraterThan(int minLength);
        ICornerQueryBuilder WithLengthSmallerThan(int maxLength);
        ICornerQueryBuilder WithHeightGraterThan(int minHeight);
        ICornerQueryBuilder WithHeightSmallerThan(int maxHeight);
        ICornerQueryBuilder WithWeightGraterThan(int minWeight);
        ICornerQueryBuilder WithWeightSmallerThan(int maxWeight);
        ICornerQueryBuilder OnlyWithSleepMode();
        ICornerQueryBuilder OnlyWithHeadrests();
        Task<List<Models.Furnitures.FurnitureImplmentation.Corner>> ToListAsync();
    }
}