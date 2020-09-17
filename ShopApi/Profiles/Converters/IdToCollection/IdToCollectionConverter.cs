using System.Linq;
using AutoMapper;
using ShopApi.DAL;
using ShopApi.Models.Furnitures;

namespace ShopApi.Profiles.Converters.IdToCollection
{
    public class IdToCollectionConverter : IIdToCollectionConverter
    {
        private readonly ShopDbContext _context;

        public IdToCollectionConverter(ShopDbContext context)
        {
            _context = context;
        }

        public Collection Convert(int id, ResolutionContext context)
        {
            return _context.CollectionItems.FirstOrDefault(c => c.Id == id);
        }
    }
}