using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopApi.DAL.Repositories.Furniture.Chair
{
    public class ChairRepository : IChairRepository
    {
        private readonly ShopDbContext _db;

        public ChairRepository(ShopDbContext db)
        {
            _db = db;
        }

        public IQueryable<Models.Furnitures.FurnitureImplmentation.Chair> GetIQuerable()
        {
            return _db.ChairItems.AsQueryable();
        }

        public async Task<IEnumerable<Models.Furnitures.FurnitureImplmentation.Chair>> GetAllAsync()
        {
            return await _db.ChairItems.Include(c => c.Collection).ToListAsync();
        }

        public async Task<Models.Furnitures.FurnitureImplmentation.Chair> GetByIdAsync(int id)
        {
            return await _db.ChairItems.Include(c => c.Collection).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> CreateAsync(Models.Furnitures.FurnitureImplmentation.Chair created)
        {
            await _db.ChairItems.AddAsync(created);
            return true;
        }

        public async Task<bool> UpdateAsync(int id, Models.Furnitures.FurnitureImplmentation.Chair updated)
        {
            var fromDb = await _db.ChairItems.FirstOrDefaultAsync(s => s.Id == id);
            if (fromDb == null){return false;}

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
            var fromDb = await _db.ChairItems.FirstOrDefaultAsync(s => s.Id == id);
            if (fromDb == null){return false;}

            _db.ChairItems.Remove(fromDb);
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}