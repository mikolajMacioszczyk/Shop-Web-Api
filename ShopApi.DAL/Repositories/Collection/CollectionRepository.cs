using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopApi.DAL.Repositories.Collection
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly ShopDbContext _db;

        public CollectionRepository(ShopDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Models.Furnitures.Collection>> GetAllAsync()
        {
            return await _db.CollectionItems.ToListAsync();
        }

        public async Task<Models.Furnitures.Collection> GetByIdAsync(int id)
        {
            return await _db.CollectionItems.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> CreateAsync(Models.Furnitures.Collection created)
        {
            await _db.CollectionItems.AddAsync(created);
            return true;
        }

        public async Task<bool> UpdateAsync(int id, Models.Furnitures.Collection updated)
        {
            var fromDb = await _db.CollectionItems.FirstOrDefaultAsync(c => c.Id == id);
            if (fromDb == null){return false;}

            fromDb.Name = updated.Name;
            fromDb.IsLimited = updated.IsLimited;
            fromDb.IsNew = updated.IsNew;
            fromDb.IsOnSale = updated.IsOnSale;

            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var fromDb = await _db.CollectionItems.FirstOrDefaultAsync(c => c.Id == id);
            if (fromDb == null){return false;}

            _db.CollectionItems.Remove(fromDb);
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}