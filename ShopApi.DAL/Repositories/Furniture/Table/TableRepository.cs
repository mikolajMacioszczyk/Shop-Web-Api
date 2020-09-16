using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopApi.DAL.Repositories.Furniture.Table
{
    public class TableRepository : ITableRepository
    {
        private readonly ShopDbContext _db;

        public TableRepository(ShopDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Models.Furnitures.FurnitureImplmentation.Table>> GetAllAsync()
        {
            return await _db.TableItems.Include(t => t.Collection).ToListAsync();
        }

        public async Task<Models.Furnitures.FurnitureImplmentation.Table> GetByIdAsync(int id)
        {
            return await _db.TableItems.Include(t => t.Collection).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> CreateAsync(Models.Furnitures.FurnitureImplmentation.Table created)
        {
            await _db.TableItems.AddAsync(created);
            return true;
        }

        public async Task<bool> UpdateAsync(int id, Models.Furnitures.FurnitureImplmentation.Table updated)
        {
            var fromDb = await _db.TableItems.FirstOrDefaultAsync(t => t.Id == id);
            if (fromDb == null){return false;}

            fromDb.Collection = updated.Collection;
            fromDb.Height = updated.Height;
            fromDb.Length = updated.Length;
            fromDb.Name = updated.Name;
            fromDb.Prize = updated.Prize;
            fromDb.Weight = updated.Weight;
            fromDb.Shape = updated.Shape;
            fromDb.IsFoldable = updated.IsFoldable;
            
            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var fromDb = await _db.TableItems.FirstOrDefaultAsync(t => t.Id == id);
            if (fromDb == null){return false;}

            _db.TableItems.Remove(fromDb);
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}