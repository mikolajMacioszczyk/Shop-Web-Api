using System;
using System.Linq;
using NUnit.Framework;
using ShopApi.Models.People;
using ShopApi.Profiles.Converters.IdToAddress;

namespace ShopApi.Tests.ConvertersUnitTests
{
    [TestFixture]
    public class IdToAddressConverterUnitTests : ShopApiTestBase
    {
        private static Random _random = new Random();
        private IIdToAddressConverter _converter;

        public IdToAddressConverterUnitTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
        }
        
        [SetUp]
        public void SetUp()
        {
            _converter = new IdToAddressConverter(_context);
        }
        
        [Test]
        public void Convert_ValidId_ShouldReturnCollection()
        {
            var address = ShopTestDatabaseInitializer.Addresses.First();
            var result = _converter.Convert(address.Id, null);
            
            Assert.NotNull(result);
            Assert.AreEqual(address.City, result.City);
            Assert.AreEqual(address.House, result.House);
            Assert.AreEqual(address.Id, result.Id);
        }
        
        [Test]
        public void Convert_InvalidId_ShouldReturnNull()
        {
            Address expected = null;
            int id = _random.Next(int.MaxValue);
            while (ShopTestDatabaseInitializer.Addresses.Any(a => a.Id == id))
            {
                id = _random.Next(int.MaxValue);
            }

            var result = _converter.Convert(id, null);
            Assert.Null(result);
        }
    }
}