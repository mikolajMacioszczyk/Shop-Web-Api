using NUnit.Framework;
using ShopApi.Models.People;
using ShopApi.Profiles.Converters.PermissionToString;

namespace ShopApi.Tests.ConvertersUnitTests
{
    [TestFixture]
    public class PermissionToStringConverterUnitTests
    {
        private IPermissionToStringConverter _converter;
        
        [SetUp]
        public void SetUp()
        {
            _converter = new PermissionToStringConverter();
        }
        
        [Test]
        public void Convert_ValidArgument()
        {
            var expected = "Full";

            var result = _converter.Convert(Permission.Full, null);
            
            Assert.AreEqual(expected, result);
        }
        
        
        [Test]
        public void Convert_0Argument_ShouldReturnString()
        {
            var expected = "Read";

            var result = _converter.Convert(0, null);
            
            Assert.AreEqual(expected, result);
        }
    }
}