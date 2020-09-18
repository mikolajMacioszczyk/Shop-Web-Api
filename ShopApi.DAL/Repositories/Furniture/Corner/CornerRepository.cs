using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopApi.DAL.Repositories.Furniture.Corner
{
    public class CornerRepository : ICornerRepository
    {
        private readonly ShopDbContext _db;

        public CornerRepository(ShopDbContext db)
        {
            _db = db;
        }

        public IQueryable<Models.Furnitures.FurnitureImplmentation.Corner> GetIQuerable()
        {
            return _db.CornerItems.AsQueryable();
        }

        public async Task<IEnumerable<Models.Furnitures.FurnitureImplmentation.Corner>> GetAllAsync()
        {
            return await _db.CornerItems.Include(c => c.Collection).ToListAsync();
        }

        public async Task<Models.Furnitures.FurnitureImplmentation.Corner> GetByIdAsync(int id)
        {
            return await _db.CornerItems.Include(c => c.Collection).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> CreateAsync(Models.Furnitures.FurnitureImplmentation.Corner created)
        {
            if (created == null)
                return false;
            await _db.CornerItems.AddAsync(created);
            return true;
        }

        public async Task<bool> UpdateAsync(int id, Models.Furnitures.FurnitureImplmentation.Corner updated)
        {
            var fromDb = await _db.CornerItems.FirstOrDefaultAsync(c => c.Id == id);
            if (fromDb == null || updated == null){return false;}

            fromDb.Collection = updated.Collection;
            fromDb.Height = updated.Height;
            fromDb.Length = updated.Length;
            fromDb.Name = updated.Name;
            fromDb.Prize = updated.Prize;
            fromDb.Weight = updated.Weight;
            fromDb.HaveHeadrests = updated.HaveHeadrests;
            fromDb.HaveSleepMode = updated.HaveSleepMode;

            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var fromDb = await _db.CornerItems.FirstOrDefaultAsync(c => c.Id == id);
            if (fromDb == null){return false;}
            
            if ((await _db.FurnitureCounts.FirstOrDefaultAsync(fc => fc.FurnitureId == id) != null))
            {
                throw new InvalidOperationException("Cannot remove furniture used in other entities in database. First remove binding within entities.");
            }

            _db.CornerItems.Remove(fromDb);
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}