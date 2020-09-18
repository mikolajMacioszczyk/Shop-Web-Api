using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ShopApi.DAL.Repositories.Furniture.Corner;
using ShopApi.Models.Furnitures.FurnitureImplmentation;

namespace ShopApi.Tests.RepositoryUnitTests.Furniture
{
    [TestFixture]
    public class CornerUnitTests : ShopApiTestBase
    {
        private static Random _random = new Random();
        private ICornerRepository _repository;

        public CornerUnitTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
            _repository = new CornerRepository(_context);
        }
        
        [Test]
        public async Task GetByIdAsyncTest_ValidId_ExpectedAddressObject()
        {
            var expected = ShopTestDatabaseInitializer.Corners.First();
            var result = await _repository.GetByIdAsync(expected.Id);
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public async Task GetByIdAsyncTest_InValidId_ExpectedNull()
        {
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Corners.Any(c => c.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            Corner result = await _repository.GetByIdAsync(id);
            Assert.AreEqual(null,result);
        }

        [Test]
        public async Task CreateAsync_ValidAddress_ShouldCreateAddress()
        {
            //arrange
            var created = new Corner()
            {
                Name = "Created",
                Collection = ShopTestDatabaseInitializer.Collections.First(),
                Height = 100,
                Length = 100,
                Prize = 100,
                Weight = 100,
                Width = 100,
                HaveHeadrests = true,
                HaveSleepMode = false
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
            var corner = ShopTestDatabaseInitializer.Corners.First();
            var updated = new Corner()
            {
                Name = "Updated",
                Collection = ShopTestDatabaseInitializer.Collections.Skip(1).First(),
                Height = 100,
                Length = 100,
                Prize = 100,
                Weight = 100,
                Width = 100,
                HaveHeadrests = true,
                HaveSleepMode = false
            };
            //act
            var result = await _repository.UpdateAsync(corner.Id, updated);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(corner.Id); 
            //assert
            Assert.True(result);
            Assert.AreEqual(fromDb.Name, updated.Name);
            Assert.AreEqual(fromDb.Collection.Id, updated.Collection.Id);
            Assert.AreEqual(fromDb.Type, updated.Type);
            Assert.AreEqual(fromDb.Height, updated.Height);
            Assert.AreEqual(fromDb.Length, updated.Length);
            Assert.AreEqual(fromDb.HaveHeadrests, updated.HaveHeadrests);
            Assert.AreEqual(fromDb.HaveSleepMode, updated.HaveSleepMode);

            await _repository.UpdateAsync(corner.Id, corner);
        }
        
        [Test]
        public async Task UpdateAsync_InvalidId_ShouldReturnFalse()
        {
            //arrange
            var updated = new Corner()
            {
                Name = "Updated",
                Collection = ShopTestDatabaseInitializer.Collections.Skip(2).First(),
                Height = 100,
                Length = 100,
                Prize = 100,
                Weight = 100,
                Width = 100,
                HaveHeadrests = false,
                HaveSleepMode = true
            };
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Corners.Any(a => a.Id == id))
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
            var corner = ShopTestDatabaseInitializer.Corners.Last();
            Corner updated = null;
            //act
            var result = await _repository.UpdateAsync(corner.Id, updated);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(corner.Id); 
            //assert
            Assert.False(result);
            Assert.AreEqual(fromDb.Name, corner.Name);
            Assert.AreEqual(fromDb.Collection.Id, corner.Collection.Id);
            Assert.AreEqual(fromDb.Height, corner.Height);
            Assert.AreEqual(fromDb.Type, corner.Type);
        }

        [Test]
        public async Task RemoveAsync_ValidId_NotBindedWithOtherEntities_ShouldRemoveID()
        {
            //arrange
            var corner = new Corner()
            {
                Name = "ToRemove",
                Collection = ShopTestDatabaseInitializer.Collections.Skip(1).First(),
                Height = 100,
                Length = 100,
                Prize = 100,
                Weight = 100,
                Width = 100,
                HaveHeadrests = true,
                HaveSleepMode = false
            };
            await _repository.CreateAsync(corner);
            await _repository.SaveChangesAsync();
            //act
            var result = await _repository.RemoveAsync(corner.Id);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(corner.Id);
            //assert
            Assert.True(result);
            Assert.Null(fromDb);
        }
        
        [Test]
        public async Task RemoveAsync_ValidId_BindedWithOtherEntities_ShouldThrowInvalidOperationException()
        {
            //arrange
            var corner = ShopTestDatabaseInitializer.Corners.Last();
            
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _repository.RemoveAsync(corner.Id) );
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(corner.Id);
            //assert
            Assert.AreEqual(corner.Name, fromDb.Name);
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