using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ShopApi.Controllers.People;
using ShopApi.DAL.Repositories.People.Customer;
using ShopApi.Models.Dtos.People.Customer;
using ShopApi.QueryBuilder.People.Customer;

namespace ShopApi.Tests.Controllers
{
    [TestFixture]
    public class CustomerControllerUnitTests : ShopApiTestBase
    {
        private ICustomerRepository _repository;
        private IMapper _mapper;
        private ICustomerQueryBuilder _queryBuilder;
        private CustomerController _controller;
        private readonly Random _random = new Random();

        public CustomerControllerUnitTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
            _repository = new CustomerRepository(_context);

            _mapper = MapperInitializer.GetMapper(_context);
        }
        
        [SetUp]
        public void SetUp()
        {
            _controller = new CustomerController(_repository, _mapper, _queryBuilder);
        }
        
        [Test]
        public async Task GetByIdAsync_ValidID_ShouldReturnAddress()
        {
            var id = ShopTestDatabaseInitializer.Customers.First().Id;
            var expectedCustomer = _mapper.Map<CustomerReadDto>(ShopTestDatabaseInitializer.Customers.First(c => c.Id == id));
            var result = (await _controller.GetByIdAsync(id)).Result;
            
            Assert.IsInstanceOf<OkObjectResult>(result);
            var asOk = result as OkObjectResult;
            var customer = (asOk.Value as CustomerReadDto);
            Assert.AreEqual(expectedCustomer.Name, customer.Name);
            Assert.AreEqual(expectedCustomer.Address.Id, customer.Address.Id);
            Assert.True(expectedCustomer.Orders.Select(o => o.Id).OrderBy(id => id)
                .SequenceEqual(customer.Orders.Select(o => o.Id).OrderBy(id => id)));
            Assert.AreEqual(expectedCustomer.Id, customer.Id);
        }

        [Test]
        public async Task GetByIdAsync_InvalidID_ShouldReturnNotFound()
        {
            var id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Customers.Any(c => c.Id == id))
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
            var customer = ShopTestDatabaseInitializer.Customers.Last();
            var copy = new CustomerUpdateDto()
            {
                Name = customer.Name,
                AddressId = customer.Address.Id,
                OrderIds = customer.Orders.Select(o => o.Id)
            };
            var update = new CustomerUpdateDto()
            {
                Name = "Updated",
                AddressId = ShopTestDatabaseInitializer.Addresses.Skip(2).First().Id,
                OrderIds = ShopTestDatabaseInitializer.Orders.Take(3).Select(o => o.Id)
            };
            
            // act
            var result = (await _controller.UpdateAsync(customer.Id, update)).Result;

            // assert
            Assert.IsInstanceOf<AcceptedResult>(result);
            var asOk = result as AcceptedResult;
            CustomerReadDto asDto = asOk.Value as CustomerReadDto;
            Assert.AreEqual(update.Name, asDto.Name);
            Assert.AreEqual(update.AddressId, asDto.Address.Id);
            Assert.True(update.OrderIds.OrderBy(id => id).SequenceEqual(
                asDto.Orders.Select(o => o.Id).OrderBy(id => id)));

            await _controller.UpdateAsync(customer.Id, copy);
        }
        
        [Test]
        public async Task UpdateAsync_InvalidId_ValidUpdateDto_ShouldReturnBadRequest()
        {
            // assert
            var id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Customers.Any(c => c.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            var update = new CustomerUpdateDto()
            {
                Name = "Updated",
                AddressId = ShopTestDatabaseInitializer.Addresses.Last().Id,
                OrderIds = ShopTestDatabaseInitializer.Orders.Skip(1).Take(2).Select(o => o.Id)
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
            var customer = new CustomerCreateDto()
            {
                Name = "Created",
                AddressId = ShopTestDatabaseInitializer.Addresses.Last().Id,
                OrderIds = ShopTestDatabaseInitializer.Orders.Take(3).Select(o => o.Id)
            };
            
            // act
            var result = (await _controller.CreateAsync(customer)).Result;

            // assert
            Assert.IsInstanceOf<CreatedResult>(result);
            var asCreated = result as CreatedResult;
            CustomerReadDto asDto = asCreated.Value as CustomerReadDto;
            Assert.AreEqual(customer.Name, asDto.Name);
            Assert.AreEqual(customer.AddressId, asDto.Address.Id);
            Assert.True(customer.OrderIds.OrderBy(id => id).SequenceEqual(
                asDto.Orders.Select(o => o.Id).OrderBy(id => id)));
        }
        
        [Test]
        public async Task DeleteAsync_ValidId_NoBindingWithOtherEntities_ShouldRemove()
        {
            // assert
            var customer = new CustomerCreateDto()
            {
                Name = "Created",
                AddressId = ShopTestDatabaseInitializer.Addresses.Last().Id,
                OrderIds = ShopTestDatabaseInitializer.Orders.Take(3).Select(o => o.Id)
            };
            var created = ((await _controller.CreateAsync(customer)).Result as CreatedResult).Value as CustomerReadDto;
            
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
            while (ShopTestDatabaseInitializer.Customers.Any(c => c.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            
            // act
            var result = (await _controller.DeleteAsync(id));
            
            // assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}