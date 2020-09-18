using NUnit.Framework;
using ShopApi.Models.People;
using ShopApi.Profiles.Converters.StringToPermission;

namespace ShopApi.Tests.ConvertersUnitTests
{
    [TestFixture]
    public class StringToPermissionConverterUnitTests
    {
        private IStringToPermissionConverter _converter;
        
        [SetUp]
        public void SetUp()
        {
            _converter = new StringToPermissionConverter();
        }
        
        [Test]
        public void Convert_ValidArgument()
        {
            var expected = Permission.Full;

            var result = _converter.Convert("Full", null);
            
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void Convert_InValidArgument_ShouldReturn_StatusRejected()
        {
            var expected = Permission.Read;

            var result = _converter.Convert("xxx", null);
            
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void Convert_NullArgument_ShouldReturn_StatusRejected()
        {
            var expected = Permission.Read;

            var result = _converter.Convert(null, null);
            
            Assert.AreEqual(expected, result);
        }
    }
}