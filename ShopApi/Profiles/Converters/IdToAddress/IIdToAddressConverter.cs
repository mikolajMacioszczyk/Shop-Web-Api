using AutoMapper;
using ShopApi.Models.People;

namespace ShopApi.Profiles.Converters.IdToAddress
{
    public interface IIdToAddressConverter : IValueConverter<int, Address>
    {
        
    }
}