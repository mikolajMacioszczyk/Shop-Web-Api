using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ShopApi.DAL.Repositories.Furniture.Table;
using ShopApi.Models.Furnitures.FurnitureImplmentation;

namespace ShopApi.Tests.RepositoryUnitTests.Furniture
{
    public class TableUnitTests : ShopApiTestBase
    {
        private static Random _random = new Random();
        private ITableRepository _repository;

        public TableUnitTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
            _repository = new TableRepository(_context);
        }
        
        [Test]
        public async Task GetByIdAsyncTest_ValidId_ExpectedAddressObject()
        {
            var expected = ShopTestDatabaseInitializer.Tables.First();
            var result = await _repository.GetByIdAsync(expected.Id);
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public async Task GetByIdAsyncTest_InValidId_ExpectedNull()
        {
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Tables.Any(c => c.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            Table result = await _repository.GetByIdAsync(id);
            Assert.AreEqual(null,result);
        }

        [Test]
        public async Task CreateAsync_ValidAddress_ShouldCreateAddress()
        {
            //arrange
            var created = new Table()
            {
                Name = "Created",
                Collection = ShopTestDatabaseInitializer.Collections.First(),
                Height = 100,
                Length = 100,
                Prize = 100,
                Weight = 100,
                Width = 100,
                Shape = "Circle",
                IsFoldable = false
            };
            //act
            var result = await _repository.CreateAsync(created);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(created.Id);
            //assert
            Assert.True(result);
            Assert.AreEqual(created.Name,fromDb.Name);
            Assert.AreEqual(created.Collection.Id,fromDb.Collection.Id);
            Assert.AreEqual(created.Shape,fromDb.Shape);
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
            var table = ShopTestDatabaseInitializer.Tables.First();
            var updated = new Table()
            {
                Name = "Updated",
                Collection = ShopTestDatabaseInitializer.Collections.Skip(1).First(),
                Height = 100,
                Length = 100,
                Prize = 100,
                Weight = 100,
                Width = 100,
                Shape = "Rectangle",
                IsFoldable = true
            };
            //act
            var result = await _repository.UpdateAsync(table.Id, updated);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(table.Id); 
            //assert
            Assert.True(result);
            Assert.AreEqual(fromDb.Name, updated.Name);
            Assert.AreEqual(fromDb.Collection.Id, updated.Collection.Id);
            Assert.AreEqual(fromDb.Type, updated.Type);
            Assert.AreEqual(fromDb.Height, updated.Height);
            Assert.AreEqual(fromDb.Length, updated.Length);
            Assert.AreEqual(fromDb.Shape, updated.Shape);
            Assert.AreEqual(fromDb.IsFoldable, updated.IsFoldable);

            await _repository.UpdateAsync(table.Id, table);
        }
        
        [Test]
        public async Task UpdateAsync_InvalidId_ShouldReturnFalse()
        {
            //arrange
            var updated = new Table()
            {
                Name = "Updated",
                Collection = ShopTestDatabaseInitializer.Collections.Skip(2).First(),
                Height = 100,
                Length = 100,
                Prize = 100,
                Weight = 100,
                Width = 100,
                Shape = "Rectangle",
                IsFoldable = true
            };
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Tables.Any(t => t.Id == id))
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
            var table = ShopTestDatabaseInitializer.Tables.Last();
            Table updated = null;
            //act
            var result = await _repository.UpdateAsync(table.Id, updated);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(table.Id); 
            //assert
            Assert.False(result);
            Assert.AreEqual(fromDb.Name, table.Name);
            Assert.AreEqual(fromDb.Collection.Id, table.Collection.Id);
            Assert.AreEqual(fromDb.Height, table.Height);
            Assert.AreEqual(fromDb.Type, table.Type);
            Assert.AreEqual(fromDb.Shape, table.Shape);
        }

        [Test]
        public async Task RemoveAsync_ValidId_NotBindedWithOtherEntities_ShouldRemoveID()
        {
            //arrange
            var table = new Table()
            {
                Name = "ToRemove",
                Collection = ShopTestDatabaseInitializer.Collections.Skip(1).First(),
                Height = 100,
                Length = 100,
                Prize = 100,
                Weight = 100,
                Width = 100,
                Shape = "Circle",
                IsFoldable = true
            };
            await _repository.CreateAsync(table);
            await _repository.SaveChangesAsync();
            //act
            var result = await _repository.RemoveAsync(table.Id);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(table.Id);
            //assert
            Assert.True(result);
            Assert.Null(fromDb);
        }
        
        [Test]
        public async Task RemoveAsync_ValidId_BindedWithOtherEntities_ShouldThrowInvalidOperationException()
        {
            //arrange
            var table = ShopTestDatabaseInitializer.Tables.Last();
            
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _repository.RemoveAsync(table.Id) );
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(table.Id);
            //assert
            Assert.AreEqual(table.Name, fromDb.Name);
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