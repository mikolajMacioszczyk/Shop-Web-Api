using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ShopApi.Controllers.Orders;
using ShopApi.DAL.Repositories.Orders;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Corner;
using ShopApi.Models.Dtos.Orders.FurnitureCountDtos;
using ShopApi.Models.Dtos.Orders.OrderDtos;
using ShopApi.Models.Orders;
using ShopApi.QueryBuilder.Furniture.Corner;
using ShopApi.QueryBuilder.Order;

namespace ShopApi.Tests.Controllers
{
    [TestFixture]
    public class OrderControllerUnitTests : ShopApiTestBase
    {
        private IOrderRepository _repository;
        private IMapper _mapper;
        private IOrderQueryBuilder _queryBuilder;
        private OrderController _controller;
        private readonly Random _random = new Random();

        public OrderControllerUnitTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
            _repository = new OrderRepository(_context);

            _mapper = MapperInitializer.GetMapper(_context);
        }
        
        [SetUp]
        public void SetUp()
        {
            _controller = new OrderController(_repository, _mapper, _queryBuilder);
        }
        
        [Test]
        public async Task GetByIdAsync_ValidID_ShouldReturnAddress()
        {
            var id = ShopTestDatabaseInitializer.Orders.First().Id;
            var expectedOrder = _mapper.Map<OrderReadDto>(ShopTestDatabaseInitializer.Orders.First(o => o.Id == id));
            var result = (await _controller.GetByIdAsync(id)).Result;
            
            Assert.IsInstanceOf<OkObjectResult>(result);
            var asOk = result as OkObjectResult;
            var order = (asOk.Value as OrderReadDto);
            Assert.AreEqual(expectedOrder.Id, order.Id);
            Assert.AreEqual(expectedOrder.Status, order.Status);
            Assert.AreEqual(expectedOrder.DateOfAdmission, order.DateOfAdmission);
            Assert.AreEqual(expectedOrder.TotalPrize, order.TotalPrize);
            Assert.True(expectedOrder.Furnitures.OrderBy(f => f.FurnitureId).SequenceEqual(order.Furnitures.OrderBy(f => f.FurnitureId)));
        }

        [Test]
        public async Task GetByIdAsync_InvalidID_ShouldReturnNotFound()
        {
            var id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Orders.Any(o => o.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            
            var result = (await _controller.GetByIdAsync(id)).Result;
            
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
        
        [Test]
        public async Task UpdateAsync_ValidId_ValidUpdateDto_ShouldUpdateDto()
        {
            // assert
            var order = ShopTestDatabaseInitializer.Orders.Skip(1).First();
            var copy = new OrderUpdateDto()
            {
                Status = order.Status.ToString(),
                Furnitures = order.Furnitures.Select(f => new FurnitureCountCreateDto(){Count = f.Count, FurnitureId = f.FurnitureId}),
                TotalPrize = order.TotalPrize,
                TotalWeight = order.TotalWeight,
                DateOfAdmission = order.DateOfAdmission,
                DateOfRealization = order.DateOfRealization
            };
            var update = new OrderUpdateDto()
            {
                Status = Status.Accepted.ToString(),
                TotalPrize = 1234,
                TotalWeight = 1234,
                DateOfAdmission = DateTime.Now,
                DateOfRealization = DateTime.Now,
                Furnitures = ShopTestDatabaseInitializer.FurnitureCounts.Take(3)
                    .Select(fc => new FurnitureCountCreateDto(){Count = fc.Count, FurnitureId = fc.FurnitureId})
            };
            
            // act
            var result = (await _controller.UpdateAsync(order.Id, update)).Result;

            // assert
            Assert.IsInstanceOf<AcceptedResult>(result);
            var asOk = result as AcceptedResult;
            OrderReadDto asDto = asOk.Value as OrderReadDto;
            Assert.AreEqual(update.Status, asDto.Status);
            Assert.AreEqual(update.TotalPrize, asDto.TotalPrize);
            Assert.AreEqual(update.DateOfAdmission, asDto.DateOfAdmission);
            Assert.True(update.Furnitures.OrderBy(f => f.FurnitureId)
                .SequenceEqual(asDto.Furnitures.Select(f => new FurnitureCountCreateDto(){Count = f.Count, FurnitureId = f.FurnitureId})
                    .OrderBy(f => f.FurnitureId)));
            
            await _controller.UpdateAsync(order.Id, copy);
        }
        
        [Test]
        public async Task UpdateAsync_InvalidId_ValidUpdateDto_ShouldReturnBadRequest()
        {
            // assert
            var id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Orders.Any(o => o.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            var update = new OrderUpdateDto()
            {
                Status = Status.Accepted.ToString(),
                TotalPrize = 1234,
                TotalWeight = 1234,
                DateOfAdmission = DateTime.Now,
                DateOfRealization = DateTime.Now,
                Furnitures = ShopTestDatabaseInitializer.FurnitureCounts.Skip(2).Take(2)
                    .Select(fc => new FurnitureCountCreateDto(){Count = fc.Count, FurnitureId = fc.FurnitureId})
            };
            
            // act
            var result = (await _controller.UpdateAsync(id, update)).Result;

            // assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        
        [Test]
        public async Task CreateAsync_ValidCreateDto_ShouldCreateDto()
        {
            // assert
            var order = new OrderCreateDto()
            {
                Status = Status.Delivered.ToString(),
                TotalPrize = 1234,
                TotalWeight = 1234,
                DateOfAdmission = DateTime.Now,
                DateOfRealization = DateTime.Now,
                Furnitures = ShopTestDatabaseInitializer.FurnitureCounts.Skip(3).Take(3)
                    .Select(fc => new FurnitureCountCreateDto(){Count = fc.Count, FurnitureId = fc.FurnitureId})
            };
            
            // act
            var result = (await _controller.CreateAsync(order)).Result;

            // assert
            Assert.IsInstanceOf<CreatedResult>(result);
            var asCreated = result as CreatedResult;
            OrderReadDto asDto = asCreated.Value as OrderReadDto;
            Assert.AreEqual(order.Status, asDto.Status);
            Assert.AreEqual(order.TotalPrize, asDto.TotalPrize);
            Assert.AreEqual(order.DateOfAdmission, asDto.DateOfAdmission);
            Assert.True(order.Furnitures.OrderBy(f => f.FurnitureId)
                .SequenceEqual(asDto.Furnitures.Select(f => new FurnitureCountCreateDto(){Count = f.Count, FurnitureId = f.FurnitureId})
                    .OrderBy(f => f.FurnitureId)));
        }
        
        [Test]
        public async Task DeleteAsync_ValidId_NoBindingWithOtherEntities_ShouldRemove()
        {
            // assert
            var order = new OrderCreateDto()
            {
                Status = Status.Delivered.ToString(),
                TotalPrize = 1234,
                TotalWeight = 1234,
                DateOfAdmission = DateTime.Now,
                DateOfRealization = DateTime.Now,
                Furnitures = ShopTestDatabaseInitializer.FurnitureCounts.Skip(1).Take(4)
                    .Select(fc => new FurnitureCountCreateDto(){Count = fc.Count, FurnitureId = fc.FurnitureId})
            };
            var created = ((await _controller.CreateAsync(order)).Result as CreatedResult).Value as OrderReadDto;
            
            // act
            var result = (await _controller.DeleteAsync(created.Id));
            
            var tryGetResult = (await _controller.GetByIdAsync(created.Id)).Result;

            // assert
            Assert.IsInstanceOf<NoContentResult>(result);
            Assert.IsInstanceOf<NotFoundResult>(tryGetResult);
        }
        
        [Test]
        public async Task DeleteAsync_InvalidIdShouldNotRemove_ShouldReturnNotFound()
        {
            // assert
            var id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Orders.Any(o => o.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            
            // act
            var result = (await _controller.DeleteAsync(id));
            
            // assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
        
        [Test]
        public async Task DeleteAsync_ValidId_BindedWithOtherEntities_ShouldNotRemove_ShouldReturnConflict()
        {
            // assert
            var order = ShopTestDatabaseInitializer.Orders.First();
            
            // act
            var result = (await _controller.DeleteAsync(order.Id));
            
            var tryGetResult = ((await _controller.GetByIdAsync(order.Id)).Result as OkObjectResult).Value as OrderReadDto;
            
            // assert
            Assert.IsInstanceOf<ConflictObjectResult>(result);
            Assert.AreEqual(order.Status, tryGetResult.Status);
            Assert.AreEqual(order.TotalPrize, tryGetResult.TotalPrize);
            Assert.AreEqual(order.DateOfAdmission, tryGetResult.DateOfAdmission);
            Assert.True(order.Furnitures.OrderBy(f => f.FurnitureId)
                .SequenceEqual(tryGetResult.Furnitures.OrderBy(f => f.FurnitureId)));
        }
    }
}