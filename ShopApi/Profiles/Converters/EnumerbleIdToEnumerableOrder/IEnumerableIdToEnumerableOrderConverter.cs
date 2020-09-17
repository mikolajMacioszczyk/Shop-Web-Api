using System.Collections.Generic;
using AutoMapper;
using ShopApi.Models.Furnitures;
using ShopApi.Models.Orders;

namespace ShopApi.Profiles.Converters.EnumerbleIdToEnumerableOrder
{
    public interface IEnumerableIdToEnumerableOrderConverter : IValueConverter<IEnumerable<int>, IEnumerable<Order>>
    {
    }
}