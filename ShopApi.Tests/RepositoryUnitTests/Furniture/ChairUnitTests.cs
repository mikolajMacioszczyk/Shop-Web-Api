using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ShopApi.DAL.Repositories.Furniture.Chair;
using ShopApi.Models.Furnitures.FurnitureImplmentation;

namespace ShopApi.Tests.RepositoryUnitTests.Furniture
{
    [TestFixture]
    public class ChairUnitTests : ShopApiTestBase
    {
        private static Random _random = new Random();
        private IChairRepository _repository;

        public ChairUnitTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
            _repository = new ChairRepository(_context);
        }
        
        [Test]
        public async Task GetByIdAsyncTest_ValidId_ExpectedAddressObject()
        {
            var expected = ShopTestDatabaseInitializer.Chairs.First();
            var result = await _repository.GetByIdAsync(expected.Id);
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public async Task GetByIdAsyncTest_InValidId_ExpectedNull()
        {
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Chairs.Any(c => c.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            Chair result = await _repository.GetByIdAsync(id);
            Assert.AreEqual(null,result);
        }

        [Test]
        public async Task CreateAsync_ValidAddress_ShouldCreateAddress()
        {
            //arrange
            var created = new Chair()
            {
                Name = "Created",
                Collection = ShopTestDatabaseInitializer.Collections.First(),
                Height = 100,
                Length = 100,
                Prize = 100,
                Weight = 100,
                Width = 100
            };
            //act
            var result = await _repository.CreateAsync(created);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(created.Id);
            //assert
            Assert.True(result);
            Assert.AreEqual(created.Name,fromDb.Name);
            Assert.AreEqual(created.Collection.Id,fromDb.Collection.Id);
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
            var chair = ShopTestDatabaseInitializer.Chairs.First();
            var updated = new Chair()
            {
                Name = "Updated",
                Collection = ShopTestDatabaseInitializer.Collections.Skip(1).First(),
                Height = 100,
                Length = 100,
                Prize = 100,
                Weight = 100,
                Width = 100
            };
            //act
            var result = await _repository.UpdateAsync(chair.Id, updated);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(chair.Id); 
            //assert
            Assert.True(result);
            Assert.AreEqual(fromDb.Name, updated.Name);
            Assert.AreEqual(fromDb.Collection.Id, updated.Collection.Id);
            Assert.AreEqual(fromDb.Type, updated.Type);
            Assert.AreEqual(fromDb.Height, updated.Height);
            Assert.AreEqual(fromDb.Length, updated.Length);

            await _repository.UpdateAsync(chair.Id, chair);
        }
        
        [Test]
        public async Task UpdateAsync_InvalidId_ShouldReturnFalse()
        {
            //arrange
            var updated = new Chair()
            {
                Name = "Updated",
                Collection = ShopTestDatabaseInitializer.Collections.Skip(2).First(),
                Height = 100,
                Length = 100,
                Prize = 100,
                Weight = 100,
                Width = 100
            };
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Chairs.Any(a => a.Id == id))
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
            var chair = ShopTestDatabaseInitializer.Chairs.Last();
            Chair updated = null;
            //act
            var result = await _repository.UpdateAsync(chair.Id, updated);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(chair.Id); 
            //assert
            Assert.False(result);
            Assert.AreEqual(fromDb.Name, chair.Name);
            Assert.AreEqual(fromDb.Collection.Id, chair.Collection.Id);
            Assert.AreEqual(fromDb.Height, chair.Height);
            Assert.AreEqual(fromDb.Type, chair.Type);
        }

        [Test]
        public async Task RemoveAsync_ValidId_NotBindedWithOtherEntities_ShouldRemoveID()
        {
            //arrange
            var chair = new Chair()
            {
                Name = "ToRemove",
                Collection = ShopTestDatabaseInitializer.Collections.Skip(1).First(),
                Height = 100,
                Length = 100,
                Prize = 100,
                Weight = 100,
                Width = 100
            };
            await _repository.CreateAsync(chair);
            await _repository.SaveChangesAsync();
            //act
            var result = await _repository.RemoveAsync(chair.Id);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(chair.Id);
            //assert
            Assert.True(result);
            Assert.Null(fromDb);
        }
        
        [Test]
        public async Task RemoveAsync_ValidId_BindedWithOtherEntities_ShouldThrowInvalidOperationException()
        {
            //arrange
            var chair = ShopTestDatabaseInitializer.Chairs.Last();
            
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _repository.RemoveAsync(chair.Id) );
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(chair.Id);
            //assert
            Assert.AreEqual(chair.Name, fromDb.Name);
        }

        
        [Test]
        public async Task RemoveAsync_InvalidId_ShouldReturnFalse()
        {
            //arrange
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Chairs.Any(a => a.Id == id))
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