using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApi.Models.People;

namespace ShopApi.DAL.Repositories.People.Emplyee
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ShopDbContext _db;

        public EmployeeRepository(ShopDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _db.EmployeeItems.Include(e => e.Address).ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _db.EmployeeItems.Include(e => e.Address)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> CreateAsync(Employee created)
        {
            await _db.EmployeeItems.AddAsync(created);
            return true;
        }

        public async Task<bool> UpdateAsync(int id, Employee updated)
        {
            var fromDb = await _db.EmployeeItems.FirstOrDefaultAsync(e => e.Id == id);
            if (fromDb == null){return false;}

            fromDb.Permission = updated.Permission;
            fromDb.Salary = updated.Salary;
            fromDb.JobTitles = updated.JobTitles;
            fromDb.DateOfBirth = updated.DateOfBirth;
            fromDb.DateOfEmployment = updated.DateOfEmployment;
            fromDb.Address = updated.Address;
            fromDb.Name = updated.Name;

            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var fromDb = await _db.EmployeeItems.FirstOrDefaultAsync(e => e.Id == id);
            if (fromDb == null){return false;}

            _db.EmployeeItems.Remove(fromDb);
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}