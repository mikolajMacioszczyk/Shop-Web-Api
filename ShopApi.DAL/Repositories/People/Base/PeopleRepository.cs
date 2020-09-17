using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApi.Models.People;

namespace ShopApi.DAL.Repositories.People.Base
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly ShopDbContext _db;

        public PeopleRepository(ShopDbContext db)
        {
            _db = db;
        }

        public IQueryable<Person> GetQuerable()
        {
            return _db.PeopleItems.Include(p => p.Address).AsQueryable();
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _db.PeopleItems.Include(p => p.Address).ToListAsync();
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await _db.PeopleItems.Include(p => p.Address).FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}