using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ShopApi.DAL.Repositories.Orders;
using ShopApi.Models.Orders;

namespace ShopApi.Tests.RepositoryUnitTests.Order
{
    public class OrderUnitTests : ShopApiTestBase
    { 
        private static Random _random = new Random();
        private IOrderRepository _repository;

        public OrderUnitTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
            _repository = new OrderRepository(_context);
        }
        
        [Test]
        public async Task GetByIdAsyncTest_ValidId_ExpectedAddressObject()
        {
            var expected = ShopTestDatabaseInitializer.Orders.First();
            var result = await _repository.GetByIdAsync(expected.Id);
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public async Task GetByIdAsyncTest_InValidId_ExpectedNull()
        {
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Orders.Any(c => c.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            Models.Orders.Order result = await _repository.GetByIdAsync(id);
            Assert.AreEqual(null,result);
        }

        [Test]
        public async Task CreateAsync_ValidAddress_ShouldCreateAddress()
        {
            //arrange
            var created = new Models.Orders.Order()
            {
                Status = Status.Accepted,
                TotalPrize = 100,
                TotalWeight = 100,
                DateOfAdmission = DateTime.Now,
                DateOfRealization = DateTime.Now.AddDays(2),
                Furnitures = new List<FurnitureCount>(ShopTestDatabaseInitializer.FurnitureCounts.Take(2))
            };
            //act
            var result = await _repository.CreateAsync(created);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(created.Id);
            //assert
            Assert.True(result);
            Assert.AreEqual(created.Status,fromDb.Status);
            Assert.AreEqual(created.TotalPrize,fromDb.TotalPrize);
            Assert.True(created.Furnitures.SequenceEqual(fromDb.Furnitures));
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
            var order = ShopTestDatabaseInitializer.Orders.First();
            var updated = new Models.Orders.Order()
            {
                Status = Status.Accepted,
                TotalPrize = 100,
                TotalWeight = 100,
                DateOfAdmission = DateTime.Now,
                DateOfRealization = DateTime.Now.AddDays(2),
                Furnitures = new List<FurnitureCount>(ShopTestDatabaseInitializer.FurnitureCounts.Skip(1).Take(2))
            };
            //act
            var result = await _repository.UpdateAsync(order.Id, updated);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(order.Id); 
            //assert
            Assert.True(result);
            Assert.AreEqual(updated.Status,fromDb.Status);
            Assert.AreEqual(updated.TotalPrize,fromDb.TotalPrize);
            Assert.AreEqual(updated.TotalWeight,fromDb.TotalWeight);
            Assert.AreEqual(updated.DateOfAdmission,fromDb.DateOfAdmission);
            Assert.AreEqual(updated.DateOfRealization,fromDb.DateOfRealization);
            Assert.True(updated.Furnitures.SequenceEqual(fromDb.Furnitures));

            await _repository.UpdateAsync(order.Id, order);
        }
        
        [Test]
        public async Task UpdateAsync_InvalidId_ShouldReturnFalse()
        {
            //arrange
            var updated = new Models.Orders.Order()
            {
                Status = Status.Accepted,
                TotalPrize = 100,
                TotalWeight = 100,
                DateOfAdmission = DateTime.Now,
                DateOfRealization = DateTime.Now.AddDays(2),
                Furnitures = new List<FurnitureCount>(ShopTestDatabaseInitializer.FurnitureCounts.Skip(1).Take(3))
            };
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Orders.Any(o => o.Id == id))
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
            var order = ShopTestDatabaseInitializer.Orders.Last();
            Models.Orders.Order updated = null;
            //act
            var result = await _repository.UpdateAsync(order.Id, updated);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(order.Id); 
            //assert
            Assert.False(result);
            Assert.AreEqual(fromDb.Status, order.Status);
            Assert.AreEqual(fromDb.TotalPrize, order.TotalPrize);
            Assert.AreEqual(fromDb.DateOfAdmission, order.DateOfAdmission);
            Assert.True(fromDb.Furnitures.SequenceEqual(order.Furnitures));
        }

        [Test]
        public async Task RemoveAsync_ValidId_NotBindedWithOtherEntities_ShouldRemoveID()
        {
            //arrange
            var order = new Models.Orders.Order()
            {
                Status = Status.Accepted,
                TotalPrize = 100,
                TotalWeight = 100,
                DateOfAdmission = DateTime.Now,
                DateOfRealization = DateTime.Now.AddDays(2),
                Furnitures = new List<FurnitureCount>(ShopTestDatabaseInitializer.FurnitureCounts.Skip(3).Take(1))
            };
            await _repository.CreateAsync(order);
            await _repository.SaveChangesAsync();
            //act
            var result = await _repository.RemoveAsync(order.Id);
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(order.Id);
            //assert
            Assert.True(result);
            Assert.Null(fromDb);
        }
        
        [Test]
        public async Task RemoveAsync_ValidId_BindedWithOtherEntities_ShouldThrowInvalidOperationException()
        {
            //arrange
            var order = ShopTestDatabaseInitializer.Orders.Last();
            
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _repository.RemoveAsync(order.Id) );
            await _repository.SaveChangesAsync();
            var fromDb = await _repository.GetByIdAsync(order.Id);
            //assert
            Assert.AreEqual(order.Status, fromDb.Status);
            Assert.AreEqual(order.DateOfRealization, fromDb.DateOfRealization);
        }

        
        [Test]
        public async Task RemoveAsync_InvalidId_ShouldReturnFalse()
        {
            //arrange
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Orders.Any(t => t.Id == id))
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