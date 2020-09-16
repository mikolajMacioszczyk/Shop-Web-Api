using System.Collections.Generic;
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
            await _db.CornerItems.AddAsync(created);
            return true;
        }

        public async Task<bool> UpdateAsync(int id, Models.Furnitures.FurnitureImplmentation.Corner updated)
        {
            var fromDb = await _db.CornerItems.FirstOrDefaultAsync(c => c.Id == id);
            if (fromDb == null){return false;}

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

            _db.CornerItems.Remove(fromDb);
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}