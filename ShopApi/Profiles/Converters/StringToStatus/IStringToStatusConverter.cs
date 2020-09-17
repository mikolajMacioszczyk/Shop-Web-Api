using AutoMapper;
using ShopApi.Models.Orders;

namespace ShopApi.Profiles.Converters.StringToStatus
{
    public interface IStringToStatusConverter : IValueConverter<string, Status>
    {
        
    }
}