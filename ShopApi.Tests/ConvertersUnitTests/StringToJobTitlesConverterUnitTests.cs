using NUnit.Framework;
using ShopApi.Models.People;
using ShopApi.Profiles.Converters.StringToJobTitles;

namespace ShopApi.Tests.ConvertersUnitTests
{
    [TestFixture]
    public class StringToJobTitlesConverterUnitTests
    {
        private IStringToJobTitlesConverter _converter;
        
        [SetUp]
        public void SetUp()
        {
            _converter = new StringToJobTitlesConverter();
        }
        
        [Test]
        public void Convert_ValidArgument()
        {
            var expected = JobTitles.Administrator;

            var result = _converter.Convert("Administrator", null);
            
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void Convert_InValidArgument_ShouldReturn_StatusRejected()
        {
            var expected = JobTitles.Seller;

            var result = _converter.Convert("xxx", null);
            
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void Convert_NullArgument_ShouldReturn_StatusRejected()
        {
            var expected = JobTitles.Seller;

            var result = _converter.Convert(null, null);
            
            Assert.AreEqual(expected, result);
        }
    }
}