using System.Collections.Generic;
using System.Threading.Tasks;
using ShopApi.QueryBuilder.Furniture.Base;

namespace ShopApi.QueryBuilder.Furniture.Chair
{
    public interface IChairQueryBuilder
    {
        IChairQueryBuilder GetAll();
        IChairQueryBuilder WithNameLike(string pattern);
        IChairQueryBuilder WithPrizeGreaterThan(double minValue);
        IChairQueryBuilder WithPrizeSmallerThan(double maxValue);
        IChairQueryBuilder WithCollection(int collectionId);
        IChairQueryBuilder WithWidthGraterThan(int minWidth);
        IChairQueryBuilder WithWidthSmallerThan(int maxWidth);
        IChairQueryBuilder WithLengthGraterThan(int minLength);
        IChairQueryBuilder WithLengthSmallerThan(int maxLength);
        IChairQueryBuilder WithHeightGraterThan(int minHeight);
        IChairQueryBuilder WithHeightSmallerThan(int maxHeight);
        IChairQueryBuilder WithWeightGraterThan(int minWeight);
        IChairQueryBuilder WithWeightSmallerThan(int maxWeight);
        Task<List<Models.Furnitures.FurnitureImplmentation.Chair>> ToListAsync();
    }
}