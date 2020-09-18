using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ShopApi.DAL.Repositories.Address;
using ShopApi.QueryBuilder.Address;

namespace ShopApi.Tests.QueryBuilderUnitTests.Address
{
    [TestFixture]
    public class AddressQueryBuilderTests : ShopApiTestBase
    {
        private IAddressQueryBuilder _queryBuilder;
        private readonly IAddressRepository _repository;

        public AddressQueryBuilderTests()
        {
            ShopTestDatabaseInitializer.Initialize(_context);
            _repository = new AddressRepository(_context);
        }
        
        [SetUp]
        public void SetUp()
        {
            _queryBuilder = new AddressQueryBuilder(_repository);
        }
        
        [Test]
        public async Task GetAll_ShouldReturnFullList()
        {
            var expected = ShopTestDatabaseInitializer.Addresses;
            var result = await _queryBuilder.GetAll().ToListAsync();
            
            Assert.True(result.OrderBy(a => a.Id).SequenceEqual(expected.OrderBy(o => o.Id)));
        }
        
        [Test]
        public async Task WithCityLike_NullPattern_ShouldReturnEmptyList()
        {
            var expected = new Models.People.Address[0];
            var result = await _queryBuilder.GetAll().WithCityLike(null).ToListAsync();
            
            Assert.True(result.SequenceEqual(expected));
        }
        
        [Test]
        public async Task WithCityLike_StartedWith_W()
        {
            var expected = ShopTestDatabaseInitializer.Addresses.Where(a => a.City.StartsWith("W"));
            var result = await _queryBuilder.GetAll().WithCityLike("W%").ToListAsync();
            
            Assert.True(result.OrderBy(a => a.Id).SequenceEqual(expected.OrderBy(o => o.Id)));
        }
        
        [Test]
        public async Task WithCityLike_ShouldContains_aw()
        {
            var expected = ShopTestDatabaseInitializer.Addresses.Where(a => a.City.Contains("aw"));
            var result = await _queryBuilder.GetAll().WithCityLike("%aw%").ToListAsync();
            
            Assert.True(result.OrderBy(a => a.Id).SequenceEqual(expected.OrderBy(o => o.Id)));
        }
        
        [Test]
        public async Task WithCityLike_ShouldEndWith_w()
        {
            var expected = ShopTestDatabaseInitializer.Addresses.Where(a => a.City.EndsWith("w"));
            var result = await _queryBuilder.GetAll().WithCityLike("%w").ToListAsync();
            
            Assert.True(result.OrderBy(a => a.Id).SequenceEqual(expected.OrderBy(o => o.Id)));
        }
        
        [Test]
        public async Task WithCityLike_WhereSecondLetterIs_r()
        {
            var expected = ShopTestDatabaseInitializer.Addresses.Where(a => a.City[1] == 'r');
            var result = await _queryBuilder.GetAll().WithCityLike("_r%").ToListAsync();
            
            Assert.True(result.OrderBy(a => a.Id).SequenceEqual(expected.OrderBy(o => o.Id)));
        }
        
        [Test]
        public async Task WithStreetLike_NullPattern_ShouldReturnEmptyList()
        {
            var expected = new Models.People.Address[0];
            var result = await _queryBuilder.GetAll().WithStreetLike(null).ToListAsync();
            
            Assert.True(result.SequenceEqual(expected));
        }
        
        [Test]
        public async Task WithStreetLike_StartedWith_A()
        {
            var expected = ShopTestDatabaseInitializer.Addresses.Where(a => a.City.StartsWith("A"));
            var result = await _queryBuilder.GetAll().WithCityLike("A%").ToListAsync();
            
            Assert.True(result.OrderBy(a => a.Id).SequenceEqual(expected.OrderBy(o => o.Id)));
        }
        
        [Test]
        public async Task WithCityLike_ShouldContainsSpace()
        {
            var expected = ShopTestDatabaseInitializer.Addresses.Where(a => a.City.Contains(" "));
            var result = await _queryBuilder.GetAll().WithCityLike("% %").ToListAsync();
            
            Assert.True(result.OrderBy(a => a.Id).SequenceEqual(expected.OrderBy(o => o.Id)));
        }
        
        [Test]
        public async Task WithStreetLike_ShouldEndWith_a()
        {
            var expected = ShopTestDatabaseInitializer.Addresses.Where(a => a.City.EndsWith("a"));
            var result = await _queryBuilder.GetAll().WithCityLike("%a").ToListAsync();
            
            Assert.True(result.OrderBy(a => a.Id).SequenceEqual(expected.OrderBy(o => o.Id)));
        }
        
        [Test]
        public async Task WithCityLike_WhereSecondLetterFromEndIs_k()
        {
            var expected = ShopTestDatabaseInitializer.Addresses.Where(a => a.City.Length > 2 && a.City[a.City.Length-2] == 'k');
            var result = await _queryBuilder.GetAll().WithCityLike("%k_").ToListAsync();
            
            Assert.True(result.OrderBy(a => a.Id).SequenceEqual(expected.OrderBy(o => o.Id)));
        }
        
        [Test]
        public async Task WithHouse()
        {
            var expected = ShopTestDatabaseInitializer.Addresses.Where(a => a.House == 15);
            var result = await _queryBuilder.GetAll().WithHouse(15).ToListAsync();
            
            Assert.True(result.OrderBy(a => a.Id).SequenceEqual(expected.OrderBy(o => o.Id)));
        }
        
        [Test]
        public async Task WithPostalCode_NullArgument()
        {
            var expected = ShopTestDatabaseInitializer.Addresses.Where(a => a.PostalCode == null);
            var result = await _queryBuilder.GetAll().WithPostalCode(null).ToListAsync();
            
            Assert.True(result.OrderBy(a => a.Id).SequenceEqual(expected.OrderBy(o => o.Id)));
        }
        
        [Test]
        public async Task WithPostalCode_ValidArgument_NoAddressMeetsCriteria()
        {
            var expected = ShopTestDatabaseInitializer.Addresses.Where(a => a.PostalCode.Equals("xxx"));
            var result = await _queryBuilder.GetAll().WithPostalCode("xxx").ToListAsync();
            
            Assert.True(result.OrderBy(a => a.Id).SequenceEqual(expected.OrderBy(o => o.Id)));
        }
        
        [Test]
        public async Task WithPostalCode_ValidArgument_SomeAddressMeetsCriteria()
        {
            var expected = ShopTestDatabaseInitializer.Addresses.Where(a => a.PostalCode.Equals("01-001"));
            var result = await _queryBuilder.GetAll().WithPostalCode("01-001").ToListAsync();
            
            Assert.True(result.OrderBy(a => a.Id).SequenceEqual(expected.OrderBy(o => o.Id)));
        }
    }
}