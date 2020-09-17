using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApi.QueryBuilder.Furniture.Sofa
{
    public interface ISofaQueryBuilder
    {
        ISofaQueryBuilder GetAll();
        ISofaQueryBuilder WithNameLike(string pattern);
        ISofaQueryBuilder WithPrizeGreaterThan(double minValue);
        ISofaQueryBuilder WithPrizeSmallerThan(double maxValue);
        ISofaQueryBuilder WithCollection(int collectionId);
        ISofaQueryBuilder WithWidthGraterThan(int minWidth);
        ISofaQueryBuilder WithWidthSmallerThan(int maxWidth);
        ISofaQueryBuilder WithLengthGraterThan(int minLength);
        ISofaQueryBuilder WithLengthSmallerThan(int maxLength);
        ISofaQueryBuilder WithHeightGraterThan(int minHeight);
        ISofaQueryBuilder WithHeightSmallerThan(int maxHeight);
        ISofaQueryBuilder WithWeightGraterThan(int minWeight);
        ISofaQueryBuilder WithWeightSmallerThan(int maxWeight);
        ISofaQueryBuilder OnlyWithSleepMode();
        ISofaQueryBuilder WithPillowsGreaterThan(int minPillows);
        ISofaQueryBuilder WithPillowsSmallerThan(int maxPillows);
        Task<List<Models.Furnitures.FurnitureImplmentation.Sofa>> ToListAsync();
    }
}