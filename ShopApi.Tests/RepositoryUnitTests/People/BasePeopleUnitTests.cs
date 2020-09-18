using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ShopApi.DAL.Repositories.People.Base;
using ShopApi.Models.People;

namespace ShopApi.Tests.RepositoryUnitTests.People
{
    [TestFixture]
    public class BasePeopleUnitTests : ShopApiTestBase
    {
        private static Random _random = new Random();
        private IPeopleRepository _repository;

        public BasePeopleUnitTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
            _repository = new PeopleRepository(_context);
        }
        
        [Test]
        public async Task GetByIdAsyncTest_ValidId_ExpectedAddressObject()
        {
            Person expected = ShopTestDatabaseInitializer.People.First();
            var result = await _repository.GetByIdAsync(expected.Id);
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public async Task GetByIdAsyncTest_InValidId_ExpectedNull()
        {
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.People.Any(p => p.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            Person result = await _repository.GetByIdAsync(id);
            Assert.AreEqual(null,result);
        }
    }
}