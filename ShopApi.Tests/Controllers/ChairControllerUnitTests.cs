using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ShopApi.Controllers.Furniture;
using ShopApi.DAL.Repositories.Furniture.Chair;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Chair;
using ShopApi.QueryBuilder.Furniture.Chair;
using ShopApi.Tests.Profiles;

namespace ShopApi.Tests.Controllers
{
    [TestFixture]
    public class ChairControllerUnitTests : ShopApiTestBase
    {
        private readonly IChairRepository _repository;
        private readonly IMapper _mockMapper;
        private readonly IChairQueryBuilder _queryBuilder;
        private ChairController _controller;
        private readonly Random _random = new Random();

        public ChairControllerUnitTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
            _repository = new ChairRepository(_context);
            _mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new FurnitureProfile(_context));
                cfg.AddProfile(new CollectionProfile());
            }).CreateMapper();
        }
        
        [SetUp]
        public void SetUp()
        {
            _controller = new ChairController(_repository, _mockMapper, _queryBuilder);
        }
        
        [Test]
        public async Task GetByIdAsync_ValidID_ShouldReturnAddress()
        {
            var id = ShopTestDatabaseInitializer.Chairs.First().Id;
            var expectedChair = _mockMapper.Map<ChairReadDto>(ShopTestDatabaseInitializer.Chairs.First(c => c.Id == id));
            var result = (await _controller.GetByIdAsync(id)).Result;
            
            Assert.IsInstanceOf<OkObjectResult>(result);
            var asOk = result as OkObjectResult;
            var collection = (asOk.Value as ChairReadDto);
            Assert.AreEqual(expectedChair.Name, collection.Name);
            Assert.AreEqual(expectedChair.Height, collection.Height);
            Assert.AreEqual(expectedChair.Collection.Id, collection.Collection.Id);
            Assert.AreEqual(expectedChair.Id, collection.Id);
        }

        [Test]
        public async Task GetByIdAsync_InvalidID_ShouldReturnNotFound()
        {
            var id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Chairs.Any(c => c.Id == id))
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
            var chair = ShopTestDatabaseInitializer.Chairs.Skip(1).First();
            var copy = new CharUpdateDto()
            {
                Name = chair.Name,
                Length = chair.Length,
                Height = chair.Height,
                Prize = chair.Prize,
                Type = chair.Type,
                Weight = chair.Weight,
                Width = chair.Width,
                CollectionId = chair.Collection.Id
            };
            var update = new CharUpdateDto()
            {
                Name = "Updated",
                Length = 1,
                Height = 1,
                Prize = 1,
                Type = "Chair",
                Weight = 1,
                Width = 1,
                CollectionId = ShopTestDatabaseInitializer.Collections.First().Id
            };
            
            // act
            var result = (await _controller.UpdateAsync(chair.Id, update)).Result;

            // assert
            Assert.IsInstanceOf<AcceptedResult>(result);
            var asOk = result as AcceptedResult;
            ChairReadDto asDto = asOk.Value as ChairReadDto;
            Assert.AreEqual(update.Name, asDto.Name);
            Assert.AreEqual(update.Height, asDto.Height);
            Assert.AreEqual(update.CollectionId, asDto.Collection.Id);
            Assert.AreEqual(update.Type, asDto.Type);

            await _controller.UpdateAsync(chair.Id, copy);
        }
        
        [Test]
        public async Task UpdateAsync_InvalidId_ValidUpdateDto_ShouldReturnBadRequest()
        {
            // assert
            var id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Chairs.Any(c => c.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            var update = new CharUpdateDto()
            {
                Name = "Updated",
                Length = 1,
                Height = 1,
                Prize = 1,
                Type = "Chair",
                Weight = 1,
                Width = 1,
                CollectionId = ShopTestDatabaseInitializer.Collections.Skip(1).First().Id
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
            var chair = new ChairCreateDto()
            {
                Name = "Created",
                Length = 1,
                Height = 1,
                Prize = 1,
                Type = "Chair",
                Weight = 1,
                Width = 1,
                CollectionId = ShopTestDatabaseInitializer.Collections.Skip(2).First().Id
            };
            
            // act
            var result = (await _controller.CreateAsync(chair)).Result;

            // assert
            Assert.IsInstanceOf<CreatedResult>(result);
            var asCreated = result as CreatedResult;
            ChairReadDto asDto = asCreated.Value as ChairReadDto;
            Assert.AreEqual(chair.Name, asDto.Name);
            Assert.AreEqual(chair.Height, asDto.Height);
            Assert.AreEqual(chair.Type, asDto.Type);
            Assert.AreEqual(chair.CollectionId, asDto.Collection.Id);
        }
        
        [Test]
        public async Task DeleteAsync_ValidId_NoBindingWithOtherEntities_ShouldRemove()
        {
            // assert
            var chair = new ChairCreateDto()
            {
                Name = "Created",
                Length = 1,
                Height = 1,
                Prize = 1,
                Type = "Chair",
                Weight = 1,
                Width = 1,
                CollectionId = ShopTestDatabaseInitializer.Collections.First().Id
            };
            var created = ((await _controller.CreateAsync(chair)).Result as CreatedResult).Value as ChairReadDto;
            
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
            while (_context.ChairItems.Any(c => c.Id == id))
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
            var chair = ShopTestDatabaseInitializer.Chairs.Skip(2).First();
            
            // act
            var result = (await _controller.DeleteAsync(chair.Id));
            
            var tryGetResult = ((await _controller.GetByIdAsync(chair.Id)).Result as OkObjectResult).Value as ChairReadDto;
            
            // assert
            Assert.IsInstanceOf<ConflictObjectResult>(result);
            Assert.AreEqual(tryGetResult.Name, chair.Name);
            Assert.AreEqual(tryGetResult.Height, chair.Height);
            Assert.AreEqual(tryGetResult.Type, chair.Type);
            Assert.AreEqual(tryGetResult.Collection.Id, chair.Collection.Id);
        }
    }
}