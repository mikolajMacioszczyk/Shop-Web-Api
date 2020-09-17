using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopApi.DAL.Repositories.Furniture.Sofa
{
    public class SofaRepository : ISofaRepository
    {
        private readonly ShopDbContext _db;

        public SofaRepository(ShopDbContext db)
        {
            _db = db;
        }

        public IQueryable<Models.Furnitures.FurnitureImplmentation.Sofa> GetIQuerable()
        {
            return _db.SofaItems.AsQueryable();
        }

        public async Task<IEnumerable<Models.Furnitures.FurnitureImplmentation.Sofa>> GetAllAsync()
        {
            return await _db.SofaItems.Include(s => s.Collection).ToListAsync();
        }

        public async Task<Models.Furnitures.FurnitureImplmentation.Sofa> GetByIdAsync(int id)
        {
            return await _db.SofaItems.Include(s => s.Collection).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> CreateAsync(Models.Furnitures.FurnitureImplmentation.Sofa created)
        {
            await _db.SofaItems.AddAsync(created);
            return true;
        }

        public async Task<bool> UpdateAsync(int id, Models.Furnitures.FurnitureImplmentation.Sofa updated)
        {
            var fromDb = await _db.SofaItems.FirstOrDefaultAsync(s => s.Id == id);
            if (fromDb == null){return false;}

            fromDb.Pillows = updated.Pillows;
            fromDb.HasSleepMode = updated.HasSleepMode;
            fromDb.Collection = updated.Collection;
            fromDb.Height = updated.Height;
            fromDb.Length = updated.Length;
            fromDb.Name = updated.Name;
            fromDb.Prize = updated.Prize;
            fromDb.Weight = updated.Weight;

            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var fromDb = await _db.SofaItems.FirstOrDefaultAsync(s => s.Id == id);
            if (fromDb == null){return false;}

            _db.SofaItems.Remove(fromDb);
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}