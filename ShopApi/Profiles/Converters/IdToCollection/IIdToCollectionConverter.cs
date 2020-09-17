using AutoMapper;
using ShopApi.Models.Furnitures;

namespace ShopApi.Profiles.Converters.IdToCollection
{
    public interface IIdToCollectionConverter : IValueConverter<int, Collection>
    {
    }
}