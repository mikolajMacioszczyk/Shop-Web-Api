using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ShopApi.DAL.Repositories.People.Customer;
using ShopApi.Models.People;

namespace ShopApi.Tests.RepositoryUnitTests.People
{
    [TestFixture]
    public class CustomerUnitTests : ShopApiTestBase
    {
        private static Random _random = new Random();
        private ICustomerRepository _repository;

        public CustomerUnitTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
            _repository = new CustomerRepository(_context);
        }
        
        [Test]
        public async Task GetByIdAsyncTest_ValidId_ExpectedAddressObject()
        {
            var expected = ShopTestDatabaseInitializer.Customers.First();
            var result = await _repository.GetByIdAsync(expected.Id);
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public async Task GetByIdAsyncTest_InValidId_ExpectedNull()
        {
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Customers.Any(c => c.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            Customer result = await _repository.GetByIdAsync(id);
            Assert.AreEqual(null,result);
        }

        [Test]
        public async Task CreateAsync_ValidAddress_ShouldCreateAddress()
        {
            //arrange
            var created = new Customer()
            {
                Name = "Created",
                Address = ShopTestDatabaseInitializer.Addresses.First(),
                Orders = ShopTestDatabaseInitializer.Orders.Take(2).ToList()
            };
            //act
            var result = await _repository.CreateAsync(created);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(created.Id);
            //assert
            Assert.True(result);
            Assert.AreEqual(created.Name,fromDb.Name);
            Assert.AreEqual(created.Address.Id,fromDb.Address.Id);
            Assert.True(fromDb.Orders.SequenceEqual(created.Orders));
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
            var customer = ShopTestDatabaseInitializer.Customers.First();
            var updated = new Customer()
            {
                Name = "Created",
                Address = ShopTestDatabaseInitializer.Addresses.Skip(1).First(),
                Orders = ShopTestDatabaseInitializer.Orders.Skip(1).Take(2).ToList()
            };
            //act
            var result = await _repository.UpdateAsync(customer.Id, updated);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(customer.Id); 
            //assert
            Assert.True(result);
            Assert.AreEqual(updated.Name,fromDb.Name);
            Assert.AreEqual(updated.Address.Id,fromDb.Address.Id);
            Assert.True(fromDb.Orders.SequenceEqual(updated.Orders));

            await _repository.UpdateAsync(customer.Id, customer);
        }
        
        [Test]
        public async Task UpdateAsync_InvalidId_ShouldReturnFalse()
        {
            //arrange
            var updated = new Customer()
            {
                Name = "Created",
                Address = ShopTestDatabaseInitializer.Addresses.Skip(2).First(),
                Orders = ShopTestDatabaseInitializer.Orders.Skip(2).Take(2).ToList()
            };
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Customers.Any(c => c.Id == id))
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
            var customer = ShopTestDatabaseInitializer.Customers.Last();
            Customer updated = null;
            //act
            var result = await _repository.UpdateAsync(customer.Id, updated);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(customer.Id); 
            //assert
            Assert.False(result);
            Assert.AreEqual(fromDb.Name, customer.Name);
            Assert.AreEqual(fromDb.Name,customer.Name);
            Assert.AreEqual(fromDb.Address.Id,customer.Address.Id);
        }

        [Test]
        public async Task RemoveAsync_ValidId_NotBindedWithOtherEntities_ShouldRemoveID()
        {
            //arrange
            var customer = new Customer()
            {
                Name = "ToRemove",
                Address = ShopTestDatabaseInitializer.Addresses.First(),
                Orders = ShopTestDatabaseInitializer.Orders.Skip(1).Take(3).ToList()
            };
            await _repository.CreateAsync(customer);
            await _repository.SaveChangesAsync();
            //act
            var result = await _repository.RemoveAsync(customer.Id);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(customer.Id);
            //assert
            Assert.True(result);
            Assert.Null(fromDb);
        }
        
        [Test]
        public async Task RemoveAsync_ValidId_NoBindedWithOtherEntities_ShouldRemove()
        {
            //arrange
            var customer = ShopTestDatabaseInitializer.Customers.Last();

            var result =  await _repository.RemoveAsync(customer.Id);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(customer.Id);
            //assert
            Assert.True(result);
            Assert.Null(fromDb);

            await _repository.CreateAsync(customer);
        }

        
        [Test]
        public async Task RemoveAsync_InvalidId_ShouldReturnFalse()
        {
            //arrange
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Tables.Any(t => t.Id == id))
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