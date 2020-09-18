using NUnit.Framework;
using ShopApi.Models.Orders;
using ShopApi.Profiles.Converters.StringToStatus;

namespace ShopApi.Tests.ConvertersUnitTests
{
    [TestFixture]
    public class StringToStatusConverterUnitTests
    {
        private IStringToStatusConverter _converter;
        
        [SetUp]
        public void SetUp()
        {
            _converter = new StringToStatusConverter();
        }
        
        [Test]
        public void Convert_ValidArgument()
        {
            var expected = Status.Delivered;

            var result = _converter.Convert("Delivered", null);
            
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void Convert_InValidArgument_ShouldReturn_StatusRejected()
        {
            var expected = Status.Rejected;

            var result = _converter.Convert("xxx", null);
            
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void Convert_NullArgument_ShouldReturn_StatusRejected()
        {
            var expected = Status.Rejected;

            var result = _converter.Convert(null, null);
            
            Assert.AreEqual(expected, result);
        }
    }
}