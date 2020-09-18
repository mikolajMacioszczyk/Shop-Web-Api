using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopApi.DAL.Repositories.People.Customer
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ShopDbContext _db;

        public CustomerRepository(ShopDbContext db)
        {
            _db = db;
        }

        public IQueryable<Models.People.Customer> GetIQuerable()
        {
            return _db.CustomerItems.AsQueryable();
        }

        public async Task<IEnumerable<Models.People.Customer>> GetAllAsync()
        {
            return await _db.CustomerItems.Include(c => c.Address)
                .Include(c => c.Orders).ToListAsync();
        }

        public async Task<Models.People.Customer> GetByIdAsync(int id)
        {
            return await _db.CustomerItems.Include(c => c.Address)
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> CreateAsync(Models.People.Customer created)
        {
            if (created == null)
                return false;
            await _db.CustomerItems.AddAsync(created);
            return true;
        }

        public async Task<bool> UpdateAsync(int id, Models.People.Customer updated)
        {
            var fromDb = await _db.CustomerItems
                .FirstOrDefaultAsync(c => c.Id == id);
            if (fromDb == null || updated == null){return false;}

            fromDb.Orders = updated.Orders;
            fromDb.Address = updated.Address;
            fromDb.Name = updated.Name;

            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var fromDb = await _db.CustomerItems.FirstOrDefaultAsync(c => c.Id == id);
            if (fromDb == null){return false;}

            _db.CustomerItems.Remove(fromDb);
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}