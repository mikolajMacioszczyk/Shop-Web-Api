using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ShopApi.Controllers.Furniture;
using ShopApi.DAL.Repositories.Furniture.Table;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Table;
using ShopApi.QueryBuilder.Furniture.Table;
using ShopApi.Tests.Profiles;

namespace ShopApi.Tests.Controllers
{
    [TestFixture]
    public class TableControllerUnitTests : ShopApiTestBase
    {
        private ITableRepository _repository;
        private IMapper _mockMapper;
        private ITableQueryBuilder _queryBuilder;
        private TableController _controller;
        private readonly Random _random = new Random();

        public TableControllerUnitTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
            _repository = new TableRepository(_context);

            _mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CollectionProfile());
                cfg.AddProfile(new FurnitureProfile(_context));
            }).CreateMapper();
        }
        
        [SetUp]
        public void SetUp()
        {
            _controller = new TableController(_repository, _mockMapper, _queryBuilder);
        }
        
        [Test]
        public async Task GetByIdAsync_ValidID_ShouldReturnAddress()
        {
            var id = ShopTestDatabaseInitializer.Tables.First().Id;
            var expectedTable = _mockMapper.Map<TableReadDto>(ShopTestDatabaseInitializer.Tables.First(t => t.Id == id));
            var result = (await _controller.GetByIdAsync(id)).Result;
            
            Assert.IsInstanceOf<OkObjectResult>(result);
            var asOk = result as OkObjectResult;
            var table = (asOk.Value as TableReadDto);
            Assert.AreEqual(expectedTable.Name, table.Name);
            Assert.AreEqual(expectedTable.Height, table.Height);
            Assert.AreEqual(expectedTable.Collection.Id, table.Collection.Id);
            Assert.AreEqual(expectedTable.Shape, table.Shape);
            Assert.AreEqual(expectedTable.Id, table.Id);
        }

        [Test]
        public async Task GetByIdAsync_InvalidID_ShouldReturnNotFound()
        {
            var id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Tables.Any(t => t.Id == id))
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
            var table = ShopTestDatabaseInitializer.Tables.Skip(1).First();
            var copy = new TableUpdateDto()
            {
                Name = table.Name,
                Length = table.Length,
                Height = table.Height,
                Prize = table.Prize,
                Type = table.Type,
                Weight = table.Weight,
                Width = table.Width,
                CollectionId = table.Collection.Id,
                Shape = table.Shape,
                IsFoldable = table.IsFoldable
            };
            var update = new TableUpdateDto()
            {
                Name = "Updated",
                Length = 1,
                Height = 1,
                Prize = 1,
                Type = "Table",
                Weight = 1,
                Width = 1,
                CollectionId = ShopTestDatabaseInitializer.Collections.First().Id,
                Shape = "Circle",
                IsFoldable = false
            };
            
            // act
            var result = (await _controller.UpdateAsync(table.Id, update)).Result;

            // assert
            Assert.IsInstanceOf<AcceptedResult>(result);
            var asOk = result as AcceptedResult;
            TableReadDto asDto = asOk.Value as TableReadDto;
            Assert.AreEqual(update.Name, asDto.Name);
            Assert.AreEqual(update.Height, asDto.Height);
            Assert.AreEqual(update.CollectionId, asDto.Collection.Id);
            Assert.AreEqual(update.Type, asDto.Type);
            Assert.AreEqual(update.Shape, asDto.Shape);

            await _controller.UpdateAsync(table.Id, copy);
        }
        
        [Test]
        public async Task UpdateAsync_InvalidId_ValidUpdateDto_ShouldReturnBadRequest()
        {
            // assert
            var id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Tables.Any(t => t.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            var update = new TableUpdateDto()
            {
                Name = "Updated",
                Length = 1,
                Height = 1,
                Prize = 1,
                Type = "Table",
                Weight = 1,
                Width = 1,
                CollectionId = ShopTestDatabaseInitializer.Collections.Skip(1).First().Id,
                Shape = "Rectangle",
                IsFoldable = true
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
            var table = new TableCreateDto()
            {
                Name = "Created",
                Length = 1,
                Height = 1,
                Prize = 1,
                Type = "Table",
                Weight = 1,
                Width = 1,
                CollectionId = ShopTestDatabaseInitializer.Collections.Skip(2).First().Id,
                Shape = "Circle",
                IsFoldable = false
            };
            
            // act
            var result = (await _controller.CreateAsync(table)).Result;

            // assert
            Assert.IsInstanceOf<CreatedResult>(result);
            var asCreated = result as CreatedResult;
            TableReadDto asDto = asCreated.Value as TableReadDto;
            Assert.AreEqual(table.Name, asDto.Name);
            Assert.AreEqual(table.Height, asDto.Height);
            Assert.AreEqual(table.Type, asDto.Type);
            Assert.AreEqual(table.CollectionId, asDto.Collection.Id);
            Assert.AreEqual(table.Shape, asDto.Shape);
        }
        
        [Test]
        public async Task DeleteAsync_ValidId_NoBindingWithOtherEntities_ShouldRemove()
        {
            // assert
            var table = new TableCreateDto()
            {
                Name = "Created",
                Length = 1,
                Height = 1,
                Prize = 1,
                Type = "Table",
                Weight = 1,
                Width = 1,
                CollectionId = ShopTestDatabaseInitializer.Collections.First().Id,
                Shape = "Rectangle",
                IsFoldable = true
            };
            var created = ((await _controller.CreateAsync(table)).Result as CreatedResult).Value as TableReadDto;
            
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
            while (ShopTestDatabaseInitializer.Tables.Any(t => t.Id == id))
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
            var table = ShopTestDatabaseInitializer.Tables.First();
            
            // act
            var result = (await _controller.DeleteAsync(table.Id));
            
            var tryGetResult = ((await _controller.GetByIdAsync(table.Id)).Result as OkObjectResult).Value as TableReadDto;
            
            // assert
            Assert.IsInstanceOf<ConflictObjectResult>(result);
            Assert.AreEqual(tryGetResult.Name, table.Name);
            Assert.AreEqual(tryGetResult.Height, table.Height);
            Assert.AreEqual(tryGetResult.Type, table.Type);
            Assert.AreEqual(tryGetResult.Collection.Id, table.Collection.Id);
            Assert.AreEqual(tryGetResult.Shape, table.Shape);
        }
    }
}