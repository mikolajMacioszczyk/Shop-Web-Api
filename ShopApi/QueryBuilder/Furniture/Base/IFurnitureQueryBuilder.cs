using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApi.QueryBuilder.Furniture.Base
{
    public interface IFurnitureQueryBuilder
    {
        IFurnitureQueryBuilder GetAll();
        IFurnitureQueryBuilder WithNameLike(string pattern);
        IFurnitureQueryBuilder WithPrizeGreaterThan(double minValue);
        IFurnitureQueryBuilder WithPrizeSmallerThan(double maxValue);
        IFurnitureQueryBuilder WithCollection(int collectionId);
        IFurnitureQueryBuilder WithWidthGraterThan(int minWidth);
        IFurnitureQueryBuilder WithWidthSmallerThan(int maxWidth);
        IFurnitureQueryBuilder WithLengthGraterThan(int minLength);
        IFurnitureQueryBuilder WithLengthSmallerThan(int maxLength);
        IFurnitureQueryBuilder WithHeightGraterThan(int minHeight);
        IFurnitureQueryBuilder WithHeightSmallerThan(int maxHeight);
        IFurnitureQueryBuilder WithWeightGraterThan(int minWeight);
        IFurnitureQueryBuilder WithWeightSmallerThan(int maxWeight);
        Task<List<Models.Furnitures.Furniture>> ToListAsync();
    }
}