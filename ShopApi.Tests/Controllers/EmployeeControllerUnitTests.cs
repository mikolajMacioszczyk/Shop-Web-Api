using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ShopApi.Controllers.People;
using ShopApi.DAL.Repositories.People.Emplyee;
using ShopApi.Models.Dtos.People.Employee;
using ShopApi.Models.People;
using ShopApi.QueryBuilder.People.Employee;

namespace ShopApi.Tests.Controllers
{
    [TestFixture]
    public class EmployeeControllerUnitTests : ShopApiTestBase
    {
        private IEmployeeRepository _repository;
        private IMapper _mockMapper;
        private IEmployeeQueryBuilder _queryBuilder;
        private EmployeeController _controller;
        private readonly Random _random = new Random();

        public EmployeeControllerUnitTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
            _repository = new EmployeeRepository(_context);
            
            _mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Profiles.PeopleProfile(_context));
                cfg.AddProfile(new Profiles.AddressProfile());
            }).CreateMapper();
        }
        
        [SetUp]
        public void SetUp()
        {
            _controller = new EmployeeController(_repository, _mockMapper, _queryBuilder);
        }
        
        [Test]
        public async Task GetByIdAsync_ValidID_ShouldReturnAddress()
        {
            var id = ShopTestDatabaseInitializer.Employees.First().Id;
            var expectedEmployee = _mockMapper.Map<EmployeeReadDto>(ShopTestDatabaseInitializer.Employees.First(e => e.Id == id));
            var result = (await _controller.GetByIdAsync(id)).Result;
            
            Assert.IsInstanceOf<OkObjectResult>(result);
            var asOk = result as OkObjectResult;
            var employee = (asOk.Value as EmployeeReadDto);
            Assert.AreEqual(expectedEmployee.Name, employee.Name);
            Assert.AreEqual(expectedEmployee.Address.Id, employee.Address.Id);
            Assert.AreEqual(expectedEmployee.Permission, employee.Permission);
            Assert.AreEqual(expectedEmployee.Salary, employee.Salary);
            Assert.AreEqual(expectedEmployee.DateOfBirth, employee.DateOfBirth);
            Assert.AreEqual(expectedEmployee.Id, employee.Id);
        }

        [Test]
        public async Task GetByIdAsync_InvalidID_ShouldReturnNotFound()
        {
            var id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Employees.Any(e => e.Id == id))
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
            var employee = ShopTestDatabaseInitializer.Employees.Last();
            var copy = new EmployeeUpdateDto()
            {
                Name = employee.Name,
                AddressId = employee.Address.Id,
                Permission = employee.Permission.ToString(),
                Salary = employee.Salary,
                JobTitles = employee.JobTitles.ToString(),
                DateOfBirth = employee.DateOfBirth,
                DateOfEmployment = employee.DateOfEmployment
            };
            var update = new EmployeeUpdateDto()
            {
                Name = "Updated",
                AddressId = ShopTestDatabaseInitializer.Addresses.Skip(2).First().Id,
                Permission = Permission.Full.ToString(),
                Salary = 1234,
                JobTitles = JobTitles.Manager.ToString(),
                DateOfBirth = DateTime.Now,
                DateOfEmployment = DateTime.Now
            };
            
            // act
            var result = (await _controller.UpdateAsync(employee.Id, update)).Result;

            // assert
            Assert.IsInstanceOf<AcceptedResult>(result);
            var asOk = result as AcceptedResult;
            EmployeeReadDto asDto = asOk.Value as EmployeeReadDto;
            Assert.AreEqual(update.Name, asDto.Name);
            Assert.AreEqual(update.JobTitles, asDto.JobTitles);
            Assert.AreEqual(update.DateOfEmployment, asDto.DateOfEmployment);
            Assert.AreEqual(update.AddressId, asDto.Address.Id);

            await _controller.UpdateAsync(employee.Id, copy);
        }
        
        [Test]
        public async Task UpdateAsync_InvalidId_ValidUpdateDto_ShouldReturnBadRequest()
        {
            // assert
            var id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Employees.Any(e => e.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            var update = new EmployeeUpdateDto()
            {
                Name = "Updated",
                AddressId = ShopTestDatabaseInitializer.Addresses.Last().Id,
                Permission = Permission.WriteAndChange.ToString(),
                Salary = 1234,
                JobTitles = JobTitles.Administrator.ToString(),
                DateOfBirth = DateTime.Now,
                DateOfEmployment = DateTime.Now
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
            var employee = new EmployeeCreateDto()
            {
                Name = "Created",
                AddressId = ShopTestDatabaseInitializer.Addresses.Last().Id,
                Permission = Permission.Write.ToString(),
                Salary = 1234,
                JobTitles = JobTitles.Bookkeeper.ToString(),
                DateOfBirth = DateTime.Now,
                DateOfEmployment = DateTime.Now
            };
            
            // act
            var result = (await _controller.CreateAsync(employee)).Result;

            // assert
            Assert.IsInstanceOf<CreatedResult>(result);
            var asCreated = result as CreatedResult;
            EmployeeReadDto asDto = asCreated.Value as EmployeeReadDto;
            Assert.AreEqual(employee.Name, asDto.Name);
            Assert.AreEqual(employee.Permission, asDto.Permission);
            Assert.AreEqual(employee.DateOfBirth, asDto.DateOfBirth);
            Assert.AreEqual(employee.AddressId, asDto.Address.Id);
        }
        
        [Test]
        public async Task DeleteAsync_ValidId_NoBindingWithOtherEntities_ShouldRemove()
        {
            // assert
            var employee = new EmployeeCreateDto()
            {
                Name = "Created",
                AddressId = ShopTestDatabaseInitializer.Addresses.Last().Id,
                Permission = Permission.Write.ToString(),
                Salary = 1234,
                JobTitles = JobTitles.Bookkeeper.ToString(),
                DateOfBirth = DateTime.Now,
                DateOfEmployment = DateTime.Now
            };
            var created = ((await _controller.CreateAsync(employee)).Result as CreatedResult).Value as EmployeeReadDto;
            
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
            while (ShopTestDatabaseInitializer.Employees.Any(e => e.Id == id))
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