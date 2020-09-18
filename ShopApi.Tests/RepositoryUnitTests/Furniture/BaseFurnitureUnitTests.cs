using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ShopApi.DAL.Repositories.Furniture.Base;

namespace ShopApi.Tests.RepositoryUnitTests.Furniture
{
    [TestFixture]
    public class BaseFurnitureUnitTests : ShopApiTestBase
    {
        private static Random _random = new Random();
        private IFurnitureRepository _repository;

        public BaseFurnitureUnitTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
            _repository = new FurnitureRepository(_context);
        }
        
        [Test]
        public async Task GetByIdAsyncTest_ValidId_ExpectedAddressObject()
        {
            Models.Furnitures.Furniture expected = ShopTestDatabaseInitializer.Furnitures.First();
            var result = await _repository.GetByIdAsync(expected.Id);
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public async Task GetByIdAsyncTest_InValidId_ExpectedNull()
        {
            int id = _random.Next(Int32.MaxValue);
            while (ShopTestDatabaseInitializer.Furnitures.Any(c => c.Id == id))
            {
                id = _random.Next(Int32.MaxValue);
            }
            Models.Furnitures.Furniture result = await _repository.GetByIdAsync(id);
            Assert.AreEqual(null,result);
        }
    }
}