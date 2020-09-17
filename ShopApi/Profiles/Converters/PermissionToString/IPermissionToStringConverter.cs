using AutoMapper;
using ShopApi.Models.People;

namespace ShopApi.Profiles.Converters.PermissionToString
{
    public interface IPermissionToStringConverter : IValueConverter<Permission, string>
    {
    }
}