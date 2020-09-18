using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopApi.DAL.Repositories.Address
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ShopDbContext _db;

        public AddressRepository(ShopDbContext db)
        {
            _db = db;
        }

        public IQueryable<Models.People.Address> GetIQuerable()
        {
            return _db.AddressItems.AsQueryable();
        }

        public async Task<IEnumerable<Models.People.Address>> GetAllAsync()
        {
            return await _db.AddressItems.ToListAsync();
        }

        public async Task<Models.People.Address> GetByIdAsync(int id)
        {
            return await _db.AddressItems.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> CreateAsync(Models.People.Address created)
        {
            if (created == null)
                return false;
            await _db.AddAsync(created);
            return true;
        }

        public async Task<bool> UpdateAsync(int id, Models.People.Address updated)
        {
            var fromDb = await _db.AddressItems.FirstOrDefaultAsync(a => a.Id == id);
            if (fromDb == null || updated == null){return false;}

            fromDb.City = updated.City;
            fromDb.House = updated.House;
            fromDb.Street = updated.Street;
            fromDb.PostalCode = updated.PostalCode;
            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var fromDb = await _db.AddressItems.FirstOrDefaultAsync(a => a.Id == id);
            if (fromDb == null){return false;}

            if (await _db.PeopleItems.FirstOrDefaultAsync(p => p.Address.Id == id) != null)
            {
                throw new InvalidOperationException("Cannot remove address used by other entities in database. First remove binding");
            }
            
            _db.AddressItems.Remove(fromDb);
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}