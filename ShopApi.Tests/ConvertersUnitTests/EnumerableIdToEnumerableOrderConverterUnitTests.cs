using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ShopApi.Models.Orders;
using ShopApi.Profiles.Converters.EnumerbleIdToEnumerableOrder;

namespace ShopApi.Tests.ConvertersUnitTests
{
    [TestFixture]
    public class EnumerableIdToEnumerableOrderConverterUnitTests : ShopApiTestBase
    {
        private IEnumerableIdToEnumerableOrderConverter _converter;

        public EnumerableIdToEnumerableOrderConverterUnitTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
        }
        
        [SetUp]
        public void SetUp()
        {
            _converter = new EnumerableIdToEnumerableOrderConverter(_context);
        }
        
        [Test]
        public void Convert_NullArgument_ShouldReturnEmptyCollection()
        {
            var expected = new List<Order>();
            var result = _converter.Convert(null, null);
            Assert.True(expected.OrderBy(o => o.Id).SequenceEqual(result.OrderBy(o => o.Id)));
        }
        
        [Test]
        public void Convert_EmptyCollectionArgument_ShouldReturnEmptyCollection()
        {
            var expected = new List<Order>();
            var result = _converter.Convert(new List<int>(), null);
            Assert.True(expected.OrderBy(o => o.Id).SequenceEqual(result.OrderBy(o => o.Id)));
        }
        
        [Test]
        public void Convert_ValidCollectionOdIds_ShouldReturnEmptyCollection()
        {
            var expected = ShopTestDatabaseInitializer.Orders;
            var ids = ShopTestDatabaseInitializer.Orders.Select(o => o.Id);
            var result = _converter.Convert(ids, null);
            Assert.True(expected.OrderBy(o => o.Id).SequenceEqual(result.OrderBy(o => o.Id)));
        }
    }
}