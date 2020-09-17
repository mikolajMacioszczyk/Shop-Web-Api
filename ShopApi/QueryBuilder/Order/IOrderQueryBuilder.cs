using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApi.QueryBuilder.Order
{
    public interface IOrderQueryBuilder
    {
        IOrderQueryBuilder GetAll();
        IOrderQueryBuilder WithTotalPrizeGreaterThan(double minTotalPrize);
        IOrderQueryBuilder WithTotalPrizeSmallerThan(double maxTotalPrize);
        IOrderQueryBuilder WithTotalWeightGreaterThan(int minTotalWeight);
        IOrderQueryBuilder WithTotalWeightSmallerThan(int maxTotalWeight);
        IOrderQueryBuilder WithStatus(string status);
        IOrderQueryBuilder WithDateOfAdmissionGreaterThan(DateTime minDate);
        IOrderQueryBuilder WithDateOfAdmissionSmallerThan(DateTime maxDate);
        IOrderQueryBuilder WithDateOfRealizationGreaterThan(DateTime minDate);
        IOrderQueryBuilder WithDateOfRealizationSmallerThan(DateTime maxDate);
        IOrderQueryBuilder WithFurniture(int[] furnitureIds);
        Task<IEnumerable<Models.Orders.Order>> ToListAsync();
    }
}