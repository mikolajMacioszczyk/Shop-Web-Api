using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ShopApi.DAL.Repositories.People.Emplyee;
using ShopApi.Models.People;

namespace ShopApi.Tests.RepositoryUnitTests.People
{
    [TestFixture]
    public class EmployeeUnitTests : ShopApiTestBase
    {
        private static Random _random = new Random();
        private IEmployeeRepository _repository;

        public EmployeeUnitTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
            _repository = new EmployeeRepository(_context);
        }
        
        [Test]
        public async Task GetByIdAsyncTest_ValidId_ExpectedAddressObject()
        {
            var expected = ShopTestDatabaseInitializer.Employees.First();
            var result = await _repository.GetByIdAsync(expected.Id);
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public async Task GetByIdAsyncTest_InValidId_ExpectedNull()
        {
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Employees.Any(e => e.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            Employee result = await _repository.GetByIdAsync(id);
            Assert.AreEqual(null,result);
        }

        [Test]
        public async Task CreateAsync_ValidAddress_ShouldCreateAddress()
        {
            //arrange
            var created = new Employee()
            {
                Name = "Created",
                Address = ShopTestDatabaseInitializer.Addresses.First(),
                Permission = Permission.Write,
                Salary = 6000,
                JobTitles = JobTitles.Administrator,
                DateOfBirth = DateTime.Now.AddDays(-2),
                DateOfEmployment = DateTime.Now
            };
            //act
            var result = await _repository.CreateAsync(created);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(created.Id);
            //assert
            Assert.True(result);
            Assert.AreEqual(created.Name,fromDb.Name);
            Assert.AreEqual(created.Address.Id,fromDb.Address.Id);
            Assert.AreEqual(created.Permission,fromDb.Permission);
            Assert.AreEqual(created.DateOfBirth,fromDb.DateOfBirth);
            Assert.AreEqual(created.Salary,fromDb.Salary);
            await _repository.RemoveAsync(fromDb.Id);
        }
        
        [Test]
        public async Task CreateAsync_Null_ShouldDoNothing()
        {
            var result = await _repository.CreateAsync(null);
            Assert.False(result);
        }

        [Test]
        public async Task UpdateAsync_ValidAddressAndId_ShouldUpdateInDatabase()
        {
            //arrange
            var employee = ShopTestDatabaseInitializer.Employees.First();
            var updated = new Employee()
            {
                Name = "Updated",
                Address = ShopTestDatabaseInitializer.Addresses.Skip(1).First(),
                Permission = Permission.WriteAndChange,
                Salary = 5000,
                JobTitles = JobTitles.Administrator,
                DateOfBirth = DateTime.Now.AddDays(-2),
                DateOfEmployment = DateTime.Now
            };
            //act
            var result = await _repository.UpdateAsync(employee.Id, updated);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(employee.Id); 
            //assert
            Assert.True(result);
            Assert.AreEqual(updated.Name,fromDb.Name);
            Assert.AreEqual(updated.Address.Id,fromDb.Address.Id);
            Assert.AreEqual(updated.Permission,fromDb.Permission);
            Assert.AreEqual(updated.DateOfBirth,fromDb.DateOfBirth);
            Assert.AreEqual(updated.Salary,fromDb.Salary);

            await _repository.UpdateAsync(employee.Id, employee);
        }
        
        [Test]
        public async Task UpdateAsync_InvalidId_ShouldReturnFalse()
        {
            //arrange
            var updated = new Employee()
            {
                Name = "Updated",
                Address = ShopTestDatabaseInitializer.Addresses.Last(),
                Permission = Permission.WriteAndChange,
                Salary = 5000,
                JobTitles = JobTitles.Administrator,
                DateOfBirth = DateTime.Now.AddDays(-2),
                DateOfEmployment = DateTime.Now
            };
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Employees.Any(e => e.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            //act
            var result = await _repository.UpdateAsync(id, updated);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(id); 
            //assert
            Assert.False(result);
            Assert.Null(fromDb);
        }
        
        [Test]
        public async Task UpdateAsync_Null_ShouldReturnFalse()
        {
            //arrange
            var employee = ShopTestDatabaseInitializer.Employees.Last();
            Employee updated = null;
            //act
            var result = await _repository.UpdateAsync(employee.Id, updated);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(employee.Id); 
            //assert
            Assert.False(result);
            Assert.AreEqual(fromDb.Name, employee.Name);
            Assert.AreEqual(fromDb.Name,employee.Name);
            Assert.AreEqual(fromDb.Address.Id,employee.Address.Id);
        }

        [Test]
        public async Task RemoveAsync_ValidId_NotBindedWithOtherEntities_ShouldRemoveID()
        {
            //arrange
            var employee = new Employee()
            {
                Name = "To Remove",
                Address = ShopTestDatabaseInitializer.Addresses.First(),
                Permission = Permission.WriteAndChange,
                Salary = 5000,
                JobTitles = JobTitles.Administrator,
                DateOfBirth = DateTime.Now.AddDays(-2),
                DateOfEmployment = DateTime.Now
            };
            await _repository.CreateAsync(employee);
            await _repository.SaveChangesAsync();
            //act
            var result = await _repository.RemoveAsync(employee.Id);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(employee.Id);
            //assert
            Assert.True(result);
            Assert.Null(fromDb);
        }
        
        [Test]
        public async Task RemoveAsync_ValidId_NoBindedWithOtherEntities_ShouldRemove()
        {
            //arrange
            var employee = ShopTestDatabaseInitializer.Employees.Last();

            var result =  await _repository.RemoveAsync(employee.Id);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(employee.Id);
            //assert
            Assert.True(result);
            Assert.Null(fromDb);

            await _repository.CreateAsync(employee);
        }

        
        [Test]
        public async Task RemoveAsync_InvalidId_ShouldReturnFalse()
        {
            //arrange
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Tables.Any(e => e.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            //act
            var result = await _repository.RemoveAsync(id);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(id);
            //assert
            Assert.False(result);
            Assert.Null(fromDb);
        }
    }
}