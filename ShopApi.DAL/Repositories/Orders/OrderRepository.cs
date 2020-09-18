using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApi.Models.Orders;

namespace ShopApi.DAL.Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopDbContext _db;

        public OrderRepository(ShopDbContext db)
        {
            _db = db;
        }

        public IQueryable<Order> GetIQuerable()
        {
            return _db.OrderItems.AsQueryable();
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _db.OrderItems.Include(o => o.Furnitures)
                .Include(o => o.Furnitures).ThenInclude(f => f.Furniture).ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _db.OrderItems.Include(o => o.Furnitures).FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<bool> CreateAsync(Order created)
        {
            if (created == null)
                return false;
            var copy = created.Furnitures;
            created.Furnitures = created.Furnitures.Where(fc => !_db.FurnitureCounts.Contains(fc)).ToList();
            await _db.OrderItems.AddAsync(created);
            created.Furnitures = copy;
            return true;
        }

        public async Task<bool> UpdateAsync(int id, Order updated)
        {
            var fromDb = await _db.OrderItems.Include(o => o.Furnitures)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (fromDb == null || updated == null){return false;}

            fromDb.Furnitures = updated.Furnitures
                .Select(f =>
                {
                    var furnitureCount =  _db.FurnitureCounts.FirstOrDefault(
                               fc => fc.Count == f.Count && fc.FurnitureId == f.FurnitureId);
                    return furnitureCount ?? f;
                }).ToList();
            fromDb.Status = updated.Status;
            fromDb.TotalPrize = updated.TotalPrize;
            fromDb.TotalWeight = updated.TotalWeight;
            fromDb.DateOfAdmission = updated.DateOfAdmission;
            fromDb.DateOfRealization = updated.DateOfRealization;

            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var fromDb = await _db.OrderItems.Include(o => o.Furnitures)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (fromDb == null){return false;}

            if ((await _db.CustomerItems.FirstOrDefaultAsync(c => c.Orders.Any(o => o.Id == id)) != null))
            {
                throw new InvalidOperationException("Cannot remove order used by other entities in database. First remove binding between entities.");
            }

            _db.OrderItems.Remove(fromDb);
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}