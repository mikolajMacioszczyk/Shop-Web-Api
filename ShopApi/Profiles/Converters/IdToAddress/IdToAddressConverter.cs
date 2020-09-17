using System.Linq;
using AutoMapper;
using ShopApi.DAL;
using ShopApi.Models.People;

namespace ShopApi.Profiles.Converters.IdToAddress
{
    public class IdToAddressConverter : IIdToAddressConverter
    {
        private readonly ShopDbContext _db;

        public IdToAddressConverter(ShopDbContext db)
        {
            _db = db;
        }

        public Address Convert(int id, ResolutionContext context)
        {
            return _db.AddressItems.FirstOrDefault(a => a.Id == id);
        }
    }
}