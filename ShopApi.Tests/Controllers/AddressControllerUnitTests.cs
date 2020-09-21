using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ShopApi.Controllers.Addresses;
using ShopApi.DAL.Repositories.Address;
using ShopApi.Models.Dtos.Address;
using ShopApi.QueryBuilder.Address;
using ShopApi.Tests.Profiles;

namespace ShopApi.Tests.Controllers
{
    [TestFixture]
    public class AddressControllerUnitTests : ShopApiTestBase
    {
        private IAddressRepository _repository;
        private IAddressQueryBuilder _queryBuilder;
        private AddressController _controller;
        private readonly Random _random = new Random();
        private readonly IMapper _mockMapper;

        public AddressControllerUnitTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
            _repository = new AddressRepository(_context);
            _mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AddressProfile());
            }).CreateMapper();
        }
        
        [SetUp]
        public void SetUp()
        {
            _controller = new AddressController(_repository, _mockMapper, _queryBuilder);
        }
        
        [Test]
        public async Task GetByIdAsync_ValidID_ShouldReturnAddress()
        {
            var id = ShopTestDatabaseInitializer.Addresses.First().Id;
            var expectedAddress = _mockMapper.Map<AddressReadDto>(ShopTestDatabaseInitializer.Addresses.First(a => a.Id == id));
            var result = (await _controller.GetByIdAsync(id)).Result;
            
            Assert.IsInstanceOf<OkObjectResult>(result);
            var asOk = result as OkObjectResult;
            var address = (asOk.Value as AddressReadDto);
            Assert.AreEqual(expectedAddress.City, address.City);
            Assert.AreEqual(expectedAddress.House, address.House);
            Assert.AreEqual(expectedAddress.Id, address.Id);
        }

        [Test]
        public async Task GetByIdAsync_InvalidID_ShouldReturnNotFound()
        {
            var id = _random.Next(Int32.MaxValue);
            while (_context.AddressItems.Any(a => a.Id == id))
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
            var address = ShopTestDatabaseInitializer.Addresses.Skip(1).First();
            var copy = new AddressUpdateDto()
            {
                City = address.City,
                House = address.House,
                Street = address.Street,
                PostalCode = address.PostalCode
            };
            var update = new AddressUpdateDto()
            {
                City = "Updated",
                House = 10,
                Street = "Updated",
                PostalCode = "Updated"
            };
            
            // act
            var result = (await _controller.UpdateAsync(address.Id, update)).Result;

            // assert
            Assert.IsInstanceOf<AcceptedResult>(result);
            var asOk = result as AcceptedResult;
            AddressReadDto asDto = asOk.Value as AddressReadDto;
            Assert.AreEqual(update.City, asDto.City);
            Assert.AreEqual(update.House, asDto.House);

            await _controller.UpdateAsync(address.Id, copy);
        }
        
        [Test]
        public async Task UpdateAsync_InvalidId_ValidUpdateDto_ShouldReturnBadRequest()
        {
            // assert
            var id = _random.Next(Int32.MaxValue);
            while (_context.AddressItems.Any(a => a.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            var update = new AddressUpdateDto()
            {
                City = "Updated",
                House = 10,
                Street = "Updated",
                PostalCode = "Updated"
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
            var address = new AddressCreateDto()
            {
                City = "Created",
                House = 100,
                Street = "Created",
                PostalCode = "Created"
            };
            
            // act
            var result = (await _controller.CreateAsync(address)).Result;

            // assert
            Assert.IsInstanceOf<CreatedResult>(result);
            var asCreated = result as CreatedResult;
            AddressReadDto asDto = asCreated.Value as AddressReadDto;
            Assert.AreEqual(address.City, asDto.City);
            Assert.AreEqual(address.House, asDto.House);
        }
        
        [Test]
        public async Task DeleteAsync_ValidId_NoBindingWithOtherEntities_ShouldRemove()
        {
            // assert
            var address = new AddressCreateDto()
            {
                City = "Created",
                House = 100,
                Street = "Created",
                PostalCode = "Created"
            };
            var created = ((await _controller.CreateAsync(address)).Result as CreatedResult).Value as AddressReadDto;
            
            // act
            var result = (await _controller.DeleteAsync(created.Id));
            
            var tyGetResult = (await _controller.GetByIdAsync(created.Id)).Result;
            

            // assert
            Assert.IsInstanceOf<NoContentResult>(result);
            Assert.IsInstanceOf<NotFoundResult>(tyGetResult);
        }
        
        [Test]
        public async Task DeleteAsync_InvalidIdShouldNotRemove_ShouldReturnNotFound()
        {
            // assert
            var id = _random.Next(Int32.MaxValue);
            while (_context.AddressItems.Any(a => a.Id == id))
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
            var address = ShopTestDatabaseInitializer.Addresses.Skip(2).First();
            
            // act
            var result = (await _controller.DeleteAsync(address.Id));
            
            var tryGetResult = ((await _controller.GetByIdAsync(address.Id)).Result as OkObjectResult).Value as AddressReadDto;
            
            // assert
            Assert.IsInstanceOf<ConflictObjectResult>(result);
            Assert.AreEqual(tryGetResult.City, address.City);
            Assert.AreEqual(tryGetResult.House, address.House);
            Assert.AreEqual(tryGetResult.PostalCode, address.PostalCode);
        }
    }
}