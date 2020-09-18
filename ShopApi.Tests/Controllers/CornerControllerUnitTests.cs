using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ShopApi.Controllers.Furniture;
using ShopApi.DAL.Repositories.Furniture.Corner;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Corner;
using ShopApi.QueryBuilder.Furniture.Corner;

namespace ShopApi.Tests.Controllers
{
    [TestFixture]
    public class CornerControllerUnitTests : ShopApiTestBase
    {
        private ICornerRepository _repository;
        private IMapper _mapper;
        private ICornerQueryBuilder _queryBuilder;
        private CornerController _controller;
        private readonly Random _random = new Random();

        public CornerControllerUnitTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
            _repository = new CornerRepository(_context);

            _mapper = MapperInitializer.GetMapper(_context);
        }
        
        [SetUp]
        public void SetUp()
        {
            _controller = new CornerController(_repository, _mapper, _queryBuilder);
        }
        
        [Test]
        public async Task GetByIdAsync_ValidID_ShouldReturnAddress()
        {
            var id = ShopTestDatabaseInitializer.Corners.First().Id;
            var expectedCorner = _mapper.Map<CornerReadDto>(ShopTestDatabaseInitializer.Corners.First(c => c.Id == id));
            var result = (await _controller.GetByIdAsync(id)).Result;
            
            Assert.IsInstanceOf<OkObjectResult>(result);
            var asOk = result as OkObjectResult;
            var corner = (asOk.Value as CornerReadDto);
            Assert.AreEqual(expectedCorner.Name, corner.Name);
            Assert.AreEqual(expectedCorner.Height, corner.Height);
            Assert.AreEqual(expectedCorner.Collection.Id, corner.Collection.Id);
            Assert.AreEqual(expectedCorner.HaveHeadrests, corner.HaveHeadrests);
            Assert.AreEqual(expectedCorner.Id, corner.Id);
        }

        [Test]
        public async Task GetByIdAsync_InvalidID_ShouldReturnNotFound()
        {
            var id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Corners.Any(c => c.Id == id))
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
            var corner = ShopTestDatabaseInitializer.Corners.Skip(1).First();
            var copy = new CornerUpdateDto()
            {
                Name = corner.Name,
                Length = corner.Length,
                Height = corner.Height,
                Prize = corner.Prize,
                Type = corner.Type,
                Weight = corner.Weight,
                Width = corner.Width,
                CollectionId = corner.Collection.Id,
                HaveHeadrests = corner.HaveHeadrests,
                HaveSleepMode = corner.HaveSleepMode
            };
            var update = new CornerUpdateDto()
            {
                Name = "Updated",
                Length = 1,
                Height = 1,
                Prize = 1,
                Type = "Corner",
                Weight = 1,
                Width = 1,
                CollectionId = ShopTestDatabaseInitializer.Collections.First().Id,
                HaveHeadrests = false,
                HaveSleepMode = false
            };
            
            // act
            var result = (await _controller.UpdateAsync(corner.Id, update)).Result;

            // assert
            Assert.IsInstanceOf<AcceptedResult>(result);
            var asOk = result as AcceptedResult;
            CornerReadDto asDto = asOk.Value as CornerReadDto;
            Assert.AreEqual(update.Name, asDto.Name);
            Assert.AreEqual(update.Height, asDto.Height);
            Assert.AreEqual(update.CollectionId, asDto.Collection.Id);
            Assert.AreEqual(update.Type, asDto.Type);
            Assert.AreEqual(update.HaveHeadrests, asDto.HaveHeadrests);

            await _controller.UpdateAsync(corner.Id, copy);
        }
        
        [Test]
        public async Task UpdateAsync_InvalidId_ValidUpdateDto_ShouldReturnBadRequest()
        {
            // assert
            var id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Corners.Any(c => c.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            var update = new CornerUpdateDto()
            {
                Name = "Updated",
                Length = 1,
                Height = 1,
                Prize = 1,
                Type = "Corner",
                Weight = 1,
                Width = 1,
                CollectionId = ShopTestDatabaseInitializer.Collections.Skip(1).First().Id,
                HaveHeadrests = false,
                HaveSleepMode = false
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
            var corner = new CornerCreateDto()
            {
                Name = "Created",
                Length = 1,
                Height = 1,
                Prize = 1,
                Type = "Corner",
                Weight = 1,
                Width = 1,
                CollectionId = ShopTestDatabaseInitializer.Collections.Skip(2).First().Id,
                HaveHeadrests = true,
                HaveSleepMode = true
            };
            
            // act
            var result = (await _controller.CreateAsync(corner)).Result;

            // assert
            Assert.IsInstanceOf<CreatedResult>(result);
            var asCreated = result as CreatedResult;
            CornerReadDto asDto = asCreated.Value as CornerReadDto;
            Assert.AreEqual(corner.Name, asDto.Name);
            Assert.AreEqual(corner.Height, asDto.Height);
            Assert.AreEqual(corner.Type, asDto.Type);
            Assert.AreEqual(corner.CollectionId, asDto.Collection.Id);
            Assert.AreEqual(corner.HaveHeadrests, asDto.HaveHeadrests);
        }
        
        [Test]
        public async Task DeleteAsync_ValidId_NoBindingWithOtherEntities_ShouldRemove()
        {
            // assert
            var corner = new CornerCreateDto()
            {
                Name = "Created",
                Length = 1,
                Height = 1,
                Prize = 1,
                Type = "Corner",
                Weight = 1,
                Width = 1,
                CollectionId = ShopTestDatabaseInitializer.Collections.First().Id,
                HaveHeadrests = false,
                HaveSleepMode = true
            };
            var created = ((await _controller.CreateAsync(corner)).Result as CreatedResult).Value as CornerReadDto;
            
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
            while (_context.CornerItems.Any(c => c.Id == id))
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
            var corner = ShopTestDatabaseInitializer.Corners.First();
            
            // act
            var result = (await _controller.DeleteAsync(corner.Id));
            
            var tryGetResult = ((await _controller.GetByIdAsync(corner.Id)).Result as OkObjectResult).Value as CornerReadDto;
            
            // assert
            Assert.IsInstanceOf<ConflictObjectResult>(result);
            Assert.AreEqual(tryGetResult.Name, corner.Name);
            Assert.AreEqual(tryGetResult.Height, corner.Height);
            Assert.AreEqual(tryGetResult.Type, corner.Type);
            Assert.AreEqual(tryGetResult.Collection.Id, corner.Collection.Id);
            Assert.AreEqual(tryGetResult.HaveHeadrests, corner.HaveHeadrests);
        }
    }
}