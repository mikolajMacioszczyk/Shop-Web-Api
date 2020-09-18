using System;
using System.Linq;
using NUnit.Framework;
using ShopApi.Models.Furnitures;
using ShopApi.Profiles.Converters.IdToCollection;

namespace ShopApi.Tests.ConvertersUnitTests
{
    [TestFixture]
    public class IdToCollectionConverterUnitTests : ShopApiTestBase
    {
        private static Random _random = new Random();
        private IIdToCollectionConverter _converter;
        
        public IdToCollectionConverterUnitTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
        }
        
        [SetUp]
        public void SetUp()
        {
            _converter = new IdToCollectionConverter(_context);
        }
        
        [Test]
        public void Convert_ValidId_ShouldReturnCollection()
        {
            var collection = ShopTestDatabaseInitializer.Collections.First();
            var result = _converter.Convert(collection.Id, null);
            
            Assert.NotNull(result);
            Assert.AreEqual(collection.Name, result.Name);
            Assert.AreEqual(collection.IsLimited, result.IsLimited);
            Assert.AreEqual(collection.Id, result.Id);
        }
        
        [Test]
        public void Convert_InvalidId_ShouldReturnNull()
        {
            Collection expected = null;
            int id = _random.Next(int.MaxValue);
            while (ShopTestDatabaseInitializer.Collections.Any(c => c.Id == id))
            {
                id = _random.Next(int.MaxValue);
            }

            var result = _converter.Convert(id, null);
            Assert.Null(result);
        }
    }
}