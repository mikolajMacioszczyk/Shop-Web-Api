using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ShopApi.DAL;
using ShopApi.Models.Orders;

namespace ShopApi.Profiles.Converters.EnumerbleIdToEnumerableOrder
{
    public class EnumerableIdToEnumerableOrderConverter : IEnumerableIdToEnumerableOrderConverter
    {
        private readonly ShopDbContext _db;

        public EnumerableIdToEnumerableOrderConverter(ShopDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Order> Convert(IEnumerable<int> sourceMember, ResolutionContext context)
        {
            List<Order> list = new List<Order>();
            if (sourceMember == null) { return list; }
            foreach (var id in sourceMember)
            {
                var fromDb = _db.OrderItems.FirstOrDefault(f => f.Id == id);
                if (fromDb != null)
                {
                    list.Add(fromDb);
                }
            }
            return list;
        }
    }
}