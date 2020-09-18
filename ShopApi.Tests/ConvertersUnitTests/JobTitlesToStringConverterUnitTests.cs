using NUnit.Framework;
using ShopApi.Models.People;
using ShopApi.Profiles.Converters.JobTitlesToString;

namespace ShopApi.Tests.ConvertersUnitTests
{
    public class JobTitlesToStringConverterUnitTests
    {
        private IJobTitlesToStringConverter _converter;
        
        [SetUp]
        public void SetUp()
        {
            _converter = new JobTitlesToStringConverter();
        }
        
        [Test]
        public void Convert_ValidArgument()
        {
            var expected = "Bookkeeper";

            var result = _converter.Convert(JobTitles.Bookkeeper, null);
            
            Assert.AreEqual(expected, result);
        }
        
        
        [Test]
        public void Convert_0Argument_ShouldReturnString()
        {
            var expected = "Administrator";

            var result = _converter.Convert(0, null);
            
            Assert.AreEqual(expected, result);
        }
    }
}