using System;
using System.Collections.Generic;
using System.Linq;
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

        public IQueryable<Models.Furnitures.Furniture> GetQuerable()
        {
            return _db.FurnitureItems.Include(f => f.Collection).AsQueryable();
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
    }
}