using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ShopApi.Controllers.Collection;
using ShopApi.DAL.Repositories.Collection;
using ShopApi.Models.Dtos.Collection;
using ShopApi.QueryBuilder.Collection;

namespace ShopApi.Tests.Controllers
{
    [TestFixture]
    public class CollectionControllerUnitTests : ShopApiTestBase
    {
        private ICollectionRepository _repository;
        private IMapper _mapper;
        private ICollectionQueryBuilder _queryBuilder;
        private CollectionController _controller;
        private readonly Random _random = new Random();

        public CollectionControllerUnitTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
            _repository = new CollectionRepository(_context);

            _mapper = MapperInitializer.GetMapper(_context);
        }
        
        [SetUp]
        public void SetUp()
        {
            _controller = new CollectionController(_repository, _mapper, _queryBuilder);
        }
        
        [Test]
        public async Task GetByIdAsync_ValidID_ShouldReturnAddress()
        {
            var id = ShopTestDatabaseInitializer.Collections.First().Id;
            var expectedCollection = _mapper.Map<CollectionReadDto>(ShopTestDatabaseInitializer.Collections.First(c => c.Id == id));
            var result = (await _controller.GetByIdAsync(id)).Result;
            
            Assert.IsInstanceOf<OkObjectResult>(result);
            var asOk = result as OkObjectResult;
            var collection = (asOk.Value as CollectionReadDto);
            Assert.AreEqual(expectedCollection.Name, collection.Name);
            Assert.AreEqual(expectedCollection.IsNew, collection.IsNew);
            Assert.AreEqual(expectedCollection.Id, collection.Id);
        }

        [Test]
        public async Task GetByIdAsync_InvalidID_ShouldReturnNotFound()
        {
            var id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Collections.Any(c => c.Id == id))
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
            var collection = ShopTestDatabaseInitializer.Collections.Skip(1).First();
            var copy = new CollectionUpdateDto()
            {
                Name = collection.Name,
                IsLimited = collection.IsLimited,
                IsNew = collection.IsNew,
                IsOnSale = collection.IsOnSale
            };
            var update = new CollectionUpdateDto()
            {
                Name = "Updated",
                IsLimited = false,
                IsNew = false,
                IsOnSale = false
            };
            
            // act
            var result = (await _controller.UpdateAsync(collection.Id, update)).Result;

            // assert
            Assert.IsInstanceOf<AcceptedResult>(result);
            var asOk = result as AcceptedResult;
            CollectionReadDto asDto = asOk.Value as CollectionReadDto;
            Assert.AreEqual(update.Name, asDto.Name);
            Assert.AreEqual(update.IsNew, asDto.IsNew);

            await _controller.UpdateAsync(collection.Id, copy);
        }
        
        [Test]
        public async Task UpdateAsync_InvalidId_ValidUpdateDto_ShouldReturnBadRequest()
        {
            // assert
            var id = _random.Next(Int32.MaxValue);
            while (_context.CollectionItems.Any(c => c.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            var update = new CollectionUpdateDto()
            {
                Name = "Updated",
                IsLimited = false,
                IsNew = false,
                IsOnSale = false
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
            var collection = new CollectionCreateDto()
            {
                Name = "Created",
                IsLimited = true,
                IsNew = true,
                IsOnSale = true
            };
            
            // act
            var result = (await _controller.CreateAsync(collection)).Result;

            // assert
            Assert.IsInstanceOf<CreatedResult>(result);
            var asCreated = result as CreatedResult;
            CollectionReadDto asDto = asCreated.Value as CollectionReadDto;
            Assert.AreEqual(collection.Name, asDto.Name);
            Assert.AreEqual(collection.IsLimited, collection.IsLimited);
        }
        
        [Test]
        public async Task DeleteAsync_ValidId_NoBindingWithOtherEntities_ShouldRemove()
        {
            // assert
            var collection = new CollectionCreateDto()
            {
                Name = "Created",
                IsLimited = true,
                IsNew = true,
                IsOnSale = true
            };
            var created = ((await _controller.CreateAsync(collection)).Result as CreatedResult).Value as CollectionReadDto;
            
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
            while (_context.CollectionItems.Any(c => c.Id == id))
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
            var collection = ShopTestDatabaseInitializer.Collections.Skip(2).First();
            
            // act
            var result = (await _controller.DeleteAsync(collection.Id));
            
            var tryGetResult = ((await _controller.GetByIdAsync(collection.Id)).Result as OkObjectResult).Value as CollectionReadDto;
            
            // assert
            Assert.IsInstanceOf<ConflictObjectResult>(result);
            Assert.AreEqual(tryGetResult.Name, collection.Name);
            Assert.AreEqual(tryGetResult.IsLimited, collection.IsLimited);
        }
    }
}