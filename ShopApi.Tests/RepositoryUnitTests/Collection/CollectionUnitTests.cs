using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ShopApi.DAL.Repositories.Collection;

namespace ShopApi.Tests.RepositoryUnitTests.Collection
{
    [TestFixture]
    public class CollectionUnitTests : ShopApiTestBase
    {
        private static Random _random = new Random();
        private ICollectionRepository _repository;

        public CollectionUnitTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
            _repository = new CollectionRepository(_context);
        }
        
        [Test]
        public async Task GetByIdAsyncTest_ValidId_ExpectedAddressObject()
        {
            var expected = ShopTestDatabaseInitializer.Collections.FirstOrDefault();
            var result = await _repository.GetByIdAsync(expected.Id);
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public async Task GetByIdAsyncTest_InValidId_ExpectedNull()
        {
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Collections.Any(c => c.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            Models.Furnitures.Collection result = await _repository.GetByIdAsync(id);
            Assert.AreEqual(null,result);
        }

        [Test]
        public async Task CreateAsync_ValidAddress_ShouldCreateAddress()
        {
            //arrange
            var created = new Models.Furnitures.Collection()
            {
                Name = "Created",
                IsLimited = true,
                IsNew = false,
                IsOnSale = true
            };
            //act
            var result = await _repository.CreateAsync(created);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(created.Id);
            //assert
            Assert.True(result);
            Assert.AreEqual(created.Name,fromDb.Name);
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
            var collection = ShopTestDatabaseInitializer.Collections.First();
            var updated = new Models.Furnitures.Collection()
            {
                Name = "Updated",
                IsLimited = true,
                IsNew = false,
                IsOnSale = true
            };
            //act
            var result = await _repository.UpdateAsync(collection.Id, updated);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(collection.Id); 
            //assert
            Assert.True(result);
            Assert.AreEqual(fromDb.Name, updated.Name);
            Assert.AreEqual(fromDb.IsLimited, updated.IsLimited);
            Assert.AreEqual(fromDb.IsNew, updated.IsNew);
            Assert.AreEqual(fromDb.IsOnSale, updated.IsOnSale);

            await _repository.UpdateAsync(collection.Id, collection);
        }
        
        [Test]
        public async Task UpdateAsync_InvalidId_ShouldReturnFalse()
        {
            //arrange
            var updated = new Models.Furnitures.Collection()
            {
                Name = "Updated",
                IsLimited = true,
                IsNew = false,
                IsOnSale = true
            };
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Collections.Any(a => a.Id == id))
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
            var collection = ShopTestDatabaseInitializer.Collections.Last();
            Models.Furnitures.Collection updated = null;
            //act
            var result = await _repository.UpdateAsync(collection.Id, updated);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(collection.Id); 
            //assert
            Assert.False(result);
            Assert.AreEqual(fromDb.Name, collection.Name);
            Assert.AreEqual(fromDb.IsLimited, collection.IsLimited);
            Assert.AreEqual(fromDb.IsNew, collection.IsNew);
            Assert.AreEqual(fromDb.IsOnSale, collection.IsOnSale);
        }

        [Test]
        public async Task RemoveAsync_ValidId_NotBindedWithOtherEntities_ShouldRemoveID()
        {
            //arrange
            var collection = new Models.Furnitures.Collection()
            {
                Name = "ToRemove",
                IsLimited = true,
                IsNew = true,
                IsOnSale = false
            };
            await _repository.CreateAsync(collection);
            await _repository.SaveChangesAsync();
            //act
            var result = await _repository.RemoveAsync(collection.Id);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(collection.Id);
            //assert
            Assert.True(result);
            Assert.Null(fromDb);
        }
        
        [Test]
        public async Task RemoveAsync_ValidId_BindedWithOtherEntities_ShouldThrowInvalidOperationException()
        {
            //arrange
            var collection = ShopTestDatabaseInitializer.Collections.Last();
            
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _repository.RemoveAsync(collection.Id) );
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(collection.Id);
            //assert
            Assert.AreEqual(collection.Name, fromDb.Name);
        }

        
        [Test]
        public async Task RemoveAsync_InvalidId_ShouldReturnFalse()
        {
            //arrange
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Collections.Any(a => a.Id == id))
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