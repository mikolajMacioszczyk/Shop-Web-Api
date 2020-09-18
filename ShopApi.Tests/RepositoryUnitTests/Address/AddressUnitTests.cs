using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ShopApi.DAL.Repositories.Address;

namespace ShopApi.Tests.RepositoryUnitTests.Address
{
    [TestFixture]
    public class AddressUnitTests : ShopApiTestBase
    {
        private static Random _random = new Random();
        private IAddressRepository _repository;

        public AddressUnitTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
            _repository = new AddressRepository(_context);
        }
        
        [Test]
        public async Task GetByIdAsyncTest_ValidId_ExpectedAddressObject()
        {
            var expected = ShopTestDatabaseInitializer.Addresses.FirstOrDefault();
            var result = await _repository.GetByIdAsync(expected.Id);
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public async Task GetByIdAsyncTest_InValidId_ExpectedNull()
        {
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Addresses.Any(a => a.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            Models.People.Address result = await _repository.GetByIdAsync(id);
            Assert.AreEqual(null,result);
        }

        [Test]
        public async Task CreateAsync_ValidAddress_ShouldCreateAddress()
        {
            //arrange
            var created = new Models.People.Address()
            {
                City = "Test City 1",
                House = 10,
                Street = "Test Street 1",
                PostalCode = "Test PostalCode"
            };
            //act
            var result = await _repository.CreateAsync(created);
            await _repository.SaveChangesAsync();
            //assert
            Assert.True(result);
            var id = created.Id;
            var fromDb = await _repository.GetByIdAsync(id);
            Assert.AreEqual(created.City,fromDb.City);
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
            var address = ShopTestDatabaseInitializer.Addresses.First();
            var updated = new Models.People.Address()
            {
                City = "Updated",
                House = 99,
                Street = "Updated",
                PostalCode = "Updated"
            };
            //act
            var result = await _repository.UpdateAsync(address.Id, updated);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(address.Id); 
            //assert
            Assert.True(result);
            Assert.AreEqual(fromDb.City, updated.City);
            Assert.AreEqual(fromDb.House, updated.House);
            Assert.AreEqual(fromDb.Street, updated.Street);
            Assert.AreEqual(fromDb.PostalCode, updated.PostalCode);

            await _repository.UpdateAsync(address.Id, address);
        }
        
        [Test]
        public async Task UpdateAsync_InvalidId_ShouldReturnFalse()
        {
            //arrange
            var updated = new Models.People.Address()
            {
                City = "Updated",
                House = 99,
                Street = "Updated",
                PostalCode = "Updated"
            };
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Addresses.Any(a => a.Id == id))
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
            var address = ShopTestDatabaseInitializer.Addresses.Last();
            Models.People.Address updated = null;
            //act
            var result = await _repository.UpdateAsync(address.Id, updated);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(address.Id); 
            //assert
            Assert.False(result);
            Assert.AreEqual(fromDb.City, address.City);
            Assert.AreEqual(fromDb.House, address.House);
            Assert.AreEqual(fromDb.Street, address.Street);
            Assert.AreEqual(fromDb.PostalCode, address.PostalCode);
        }

        [Test]
        public async Task RemoveAsync_ValidId_NotBindedWithOtherEntities_ShouldRemoveID()
        {
            //arrange
            var address = new Models.People.Address()
            {
                City = "ToRemove",
                House = 1,
                Street = "To Remove",
                PostalCode = "To Remove"
            };
            await _repository.CreateAsync(address);
            await _repository.SaveChangesAsync();
            //act
            var result = await _repository.RemoveAsync(address.Id);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(address.Id);
            //assert
            Assert.True(result);
            Assert.Null(fromDb);
        }
        
        [Test]
        public async Task RemoveAsync_ValidId_BindedWithOtherEntities_ShouldThrowInvalidOperationException()
        {
            //arrange
            var address = ShopTestDatabaseInitializer.Addresses.Last();
            
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _repository.RemoveAsync(address.Id) );
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(address.Id);
            //assert
            Assert.AreEqual(fromDb.City, address.City);
        }

        
        [Test]
        public async Task RemoveAsync_InvalidId_ShouldReturnFalse()
        {
            //arrange
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Addresses.Any(a => a.Id == id))
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