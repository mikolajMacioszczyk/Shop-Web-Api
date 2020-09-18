using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ShopApi.DAL.Repositories.Furniture.Sofa;
using ShopApi.Models.Furnitures.FurnitureImplmentation;

namespace ShopApi.Tests.RepositoryUnitTests.Furniture
{
    public class SofaUnitTests : ShopApiTestBase
    {
        private static Random _random = new Random();
        private ISofaRepository _repository;

        public SofaUnitTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
            _repository = new SofaRepository(_context);
        }
        
        [Test]
        public async Task GetByIdAsyncTest_ValidId_ExpectedAddressObject()
        {
            var expected = ShopTestDatabaseInitializer.Sofas.First();
            var result = await _repository.GetByIdAsync(expected.Id);
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public async Task GetByIdAsyncTest_InValidId_ExpectedNull()
        {
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Sofas.Any(c => c.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            Sofa result = await _repository.GetByIdAsync(id);
            Assert.AreEqual(null,result);
        }

        [Test]
        public async Task CreateAsync_ValidAddress_ShouldCreateAddress()
        {
            //arrange
            var created = new Sofa()
            {
                Name = "Created",
                Collection = ShopTestDatabaseInitializer.Collections.First(),
                Height = 100,
                Length = 100,
                Prize = 100,
                Weight = 100,
                Width = 100,
                Pillows = 2,
                HasSleepMode = true
            };
            //act
            var result = await _repository.CreateAsync(created);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(created.Id);
            //assert
            Assert.True(result);
            Assert.AreEqual(created.Name,fromDb.Name);
            Assert.AreEqual(created.Collection.Id,fromDb.Collection.Id);
            Assert.AreEqual(created.Pillows,fromDb.Pillows);
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
            var sofa = ShopTestDatabaseInitializer.Sofas.First();
            var updated = new Sofa()
            {
                Name = "Updated",
                Collection = ShopTestDatabaseInitializer.Collections.Skip(1).First(),
                Height = 100,
                Length = 100,
                Prize = 100,
                Weight = 100,
                Width = 100,
                Pillows = 2,
                HasSleepMode = true
            };
            //act
            var result = await _repository.UpdateAsync(sofa.Id, updated);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(sofa.Id); 
            //assert
            Assert.True(result);
            Assert.AreEqual(fromDb.Name, updated.Name);
            Assert.AreEqual(fromDb.Collection.Id, updated.Collection.Id);
            Assert.AreEqual(fromDb.Type, updated.Type);
            Assert.AreEqual(fromDb.Height, updated.Height);
            Assert.AreEqual(fromDb.Length, updated.Length);
            Assert.AreEqual(fromDb.Pillows, updated.Pillows);
            Assert.AreEqual(fromDb.HasSleepMode, updated.HasSleepMode);

            await _repository.UpdateAsync(sofa.Id, sofa);
        }
        
        [Test]
        public async Task UpdateAsync_InvalidId_ShouldReturnFalse()
        {
            //arrange
            var updated = new Sofa()
            {
                Name = "Updated",
                Collection = ShopTestDatabaseInitializer.Collections.Skip(2).First(),
                Height = 100,
                Length = 100,
                Prize = 100,
                Weight = 100,
                Width = 100,
                Pillows = 4,
                HasSleepMode = true
            };
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Sofas.Any(s => s.Id == id))
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
            var sofa = ShopTestDatabaseInitializer.Sofas.Last();
            Sofa updated = null;
            //act
            var result = await _repository.UpdateAsync(sofa.Id, updated);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(sofa.Id); 
            //assert
            Assert.False(result);
            Assert.AreEqual(fromDb.Name, sofa.Name);
            Assert.AreEqual(fromDb.Collection.Id, sofa.Collection.Id);
            Assert.AreEqual(fromDb.Height, sofa.Height);
            Assert.AreEqual(fromDb.Type, sofa.Type);
            Assert.AreEqual(fromDb.Pillows, sofa.Pillows);
        }

        [Test]
        public async Task RemoveAsync_ValidId_NotBindedWithOtherEntities_ShouldRemoveID()
        {
            //arrange
            var sofa = new Sofa()
            {
                Name = "ToRemove",
                Collection = ShopTestDatabaseInitializer.Collections.Skip(1).First(),
                Height = 100,
                Length = 100,
                Prize = 100,
                Weight = 100,
                Width = 100,
                Pillows = 4,
                HasSleepMode = true
            };
            await _repository.CreateAsync(sofa);
            await _repository.SaveChangesAsync();
            //act
            var result = await _repository.RemoveAsync(sofa.Id);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(sofa.Id);
            //assert
            Assert.True(result);
            Assert.Null(fromDb);
        }
        
        [Test]
        public async Task RemoveAsync_ValidId_BindedWithOtherEntities_ShouldThrowInvalidOperationException()
        {
            //arrange
            var sofa = ShopTestDatabaseInitializer.Sofas.Last();
            
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _repository.RemoveAsync(sofa.Id) );
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(sofa.Id);
            //assert
            Assert.AreEqual(sofa.Name, fromDb.Name);
        }

        
        [Test]
        public async Task RemoveAsync_InvalidId_ShouldReturnFalse()
        {
            //arrange
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Sofas.Any(s => s.Id == id))
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