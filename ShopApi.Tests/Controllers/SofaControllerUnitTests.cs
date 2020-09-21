using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ShopApi.Controllers.Furniture;
using ShopApi.DAL.Repositories.Furniture.Sofa;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Sofa;
using ShopApi.QueryBuilder.Furniture.Sofa;

namespace ShopApi.Tests.Controllers
{
    [TestFixture]
    public class SofaControllerUnitTests : ShopApiTestBase
    {
        private ISofaRepository _repository;
        private IMapper _mockMapper;
        private ISofaQueryBuilder _queryBuilder;
        private SofaController _controller;
        private readonly Random _random = new Random();

        public SofaControllerUnitTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
            _repository = new SofaRepository(_context);

            _mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Profiles.CollectionProfile());
                cfg.AddProfile(new Profiles.FurnitureProfile(_context));
            }).CreateMapper();
        }
        
        [SetUp]
        public void SetUp()
        {
            _controller = new SofaController(_repository, _mockMapper, _queryBuilder);
        }
        
        [Test]
        public async Task GetByIdAsync_ValidID_ShouldReturnAddress()
        {
            var id = ShopTestDatabaseInitializer.Sofas.First().Id;
            var expectedSofa = _mockMapper.Map<SofaReadDto>(ShopTestDatabaseInitializer.Sofas.First(s => s.Id == id));
            var result = (await _controller.GetByIdAsync(id)).Result;
            
            Assert.IsInstanceOf<OkObjectResult>(result);
            var asOk = result as OkObjectResult;
            var sofa = (asOk.Value as SofaReadDto);
            Assert.AreEqual(expectedSofa.Name, sofa.Name);
            Assert.AreEqual(expectedSofa.Height, sofa.Height);
            Assert.AreEqual(expectedSofa.Collection.Id, sofa.Collection.Id);
            Assert.AreEqual(expectedSofa.Pillows, sofa.Pillows);
            Assert.AreEqual(expectedSofa.Id, sofa.Id);
        }

        [Test]
        public async Task GetByIdAsync_InvalidID_ShouldReturnNotFound()
        {
            var id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Sofas.Any(s => s.Id == id))
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
            var sofa = ShopTestDatabaseInitializer.Sofas.Skip(1).First();
            var copy = new SofaUpdateDto()
            {
                Name = sofa.Name,
                Length = sofa.Length,
                Height = sofa.Height,
                Prize = sofa.Prize,
                Type = sofa.Type,
                Weight = sofa.Weight,
                Width = sofa.Width,
                CollectionId = sofa.Collection.Id,
                Pillows = sofa.Pillows,
                HasSleepMode = sofa.HasSleepMode
            };
            var update = new SofaUpdateDto()
            {
                Name = "Updated",
                Length = 1,
                Height = 1,
                Prize = 1,
                Type = "Sofa",
                Weight = 1,
                Width = 1,
                CollectionId = ShopTestDatabaseInitializer.Collections.First().Id,
                Pillows = 4,
                HasSleepMode = true
            };
            
            // act
            var result = (await _controller.UpdateAsync(sofa.Id, update)).Result;

            // assert
            Assert.IsInstanceOf<AcceptedResult>(result);
            var asOk = result as AcceptedResult;
            SofaReadDto asDto = asOk.Value as SofaReadDto;
            Assert.AreEqual(update.Name, asDto.Name);
            Assert.AreEqual(update.Height, asDto.Height);
            Assert.AreEqual(update.CollectionId, asDto.Collection.Id);
            Assert.AreEqual(update.Type, asDto.Type);
            Assert.AreEqual(update.Pillows, asDto.Pillows);

            await _controller.UpdateAsync(sofa.Id, copy);
        }
        
        [Test]
        public async Task UpdateAsync_InvalidId_ValidUpdateDto_ShouldReturnBadRequest()
        {
            // assert
            var id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Sofas.Any(s => s.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            var update = new SofaUpdateDto()
            {
                Name = "Updated",
                Length = 1,
                Height = 1,
                Prize = 1,
                Type = "Sofa",
                Weight = 1,
                Width = 1,
                CollectionId = ShopTestDatabaseInitializer.Collections.Skip(1).First().Id,
                Pillows = 4,
                HasSleepMode = false
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
            var sofa = new SofaCreateDto()
            {
                Name = "Created",
                Length = 1,
                Height = 1,
                Prize = 1,
                Type = "Sofa",
                Weight = 1,
                Width = 1,
                CollectionId = ShopTestDatabaseInitializer.Collections.Skip(2).First().Id,
                Pillows = 4,
                HasSleepMode = true
            };
            
            // act
            var result = (await _controller.CreateAsync(sofa)).Result;

            // assert
            Assert.IsInstanceOf<CreatedResult>(result);
            var asCreated = result as CreatedResult;
            SofaReadDto asDto = asCreated.Value as SofaReadDto;
            Assert.AreEqual(sofa.Name, asDto.Name);
            Assert.AreEqual(sofa.Height, asDto.Height);
            Assert.AreEqual(sofa.Type, asDto.Type);
            Assert.AreEqual(sofa.CollectionId, asDto.Collection.Id);
            Assert.AreEqual(sofa.Pillows, asDto.Pillows);
        }
        
        [Test]
        public async Task DeleteAsync_ValidId_NoBindingWithOtherEntities_ShouldRemove()
        {
            // assert
            var sofa = new SofaCreateDto()
            {
                Name = "Created",
                Length = 1,
                Height = 1,
                Prize = 1,
                Type = "Sofa",
                Weight = 1,
                Width = 1,
                CollectionId = ShopTestDatabaseInitializer.Collections.First().Id,
                Pillows = 2,
                HasSleepMode = true
            };
            var created = ((await _controller.CreateAsync(sofa)).Result as CreatedResult).Value as SofaReadDto;
            
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
            while (_context.SofaItems.Any(s => s.Id == id))
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
            var sofa = ShopTestDatabaseInitializer.Sofas.First();
            
            // act
            var result = (await _controller.DeleteAsync(sofa.Id));
            
            var tryGetResult = ((await _controller.GetByIdAsync(sofa.Id)).Result as OkObjectResult).Value as SofaReadDto;
            
            // assert
            Assert.IsInstanceOf<ConflictObjectResult>(result);
            Assert.AreEqual(tryGetResult.Name, sofa.Name);
            Assert.AreEqual(tryGetResult.Height, sofa.Height);
            Assert.AreEqual(tryGetResult.Type, sofa.Type);
            Assert.AreEqual(tryGetResult.Collection.Id, sofa.Collection.Id);
            Assert.AreEqual(tryGetResult.Pillows, sofa.Pillows);
        }
    }
}