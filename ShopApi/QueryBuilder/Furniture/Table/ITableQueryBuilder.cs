using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApi.QueryBuilder.Furniture.Table
{
    public interface ITableQueryBuilder
    {
        ITableQueryBuilder GetAll();
        ITableQueryBuilder WithNameLike(string pattern);
        ITableQueryBuilder WithPrizeGreaterThan(double minValue);
        ITableQueryBuilder WithPrizeSmallerThan(double maxValue);
        ITableQueryBuilder WithCollection(int collectionId);
        ITableQueryBuilder WithWidthGraterThan(int minWidth);
        ITableQueryBuilder WithWidthSmallerThan(int maxWidth);
        ITableQueryBuilder WithLengthGraterThan(int minLength);
        ITableQueryBuilder WithLengthSmallerThan(int maxLength);
        ITableQueryBuilder WithHeightGraterThan(int minHeight);
        ITableQueryBuilder WithHeightSmallerThan(int maxHeight);
        ITableQueryBuilder WithWeightGraterThan(int minWeight);
        ITableQueryBuilder WithWeightSmallerThan(int maxWeight);
        ITableQueryBuilder OnlyFoldable();
        ITableQueryBuilder WithShapeLike(string pattern);
        Task<List<Models.Furnitures.FurnitureImplmentation.Table>> ToListAsync();
    }
}