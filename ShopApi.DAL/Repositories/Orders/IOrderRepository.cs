using System.Collections.Generic;
using ShopApi.Models.Orders;

namespace ShopApi.DAL.Repositories.Orders
{
    public interface IOrderRepository : IRepository<Order>
    {
        public IEnumerable<FurnitureCount> UpdateFurnitureCount(IEnumerable<FurnitureCount> updated);
    }
}