using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopApi.DAL.Repositories.Furniture.Base
{
    public class FurnitureRepository : IFurnitureRepository
    {
        private readonly ShopDbContext _db;

        public FurnitureRepository(ShopDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Models.Furnitures.Furniture>> GetAllAsync()
        {
            return await _db.FurnitureItems.Include(f => f.Collection).ToListAsync();
        }

        public async Task<Models.Furnitures.Furniture> GetByIdAsync(int id)
        {
            return await _db.FurnitureItems.Include(f => f.Collection)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<bool> CreateAsync(Models.Furnitures.Furniture created)
        {
            throw new NotSupportedException();
        }

        public Task<bool> UpdateAsync(int id, Models.Furnitures.Furniture updated)
        {
            throw new NotSupportedException();
        }

        public Task<bool> RemoveAsync(int id)
        {
            throw new NotSupportedException();
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}