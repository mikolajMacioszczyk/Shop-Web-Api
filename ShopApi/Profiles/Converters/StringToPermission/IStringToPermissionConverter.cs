using AutoMapper;
using ShopApi.Models.People;

namespace ShopApi.Profiles.Converters.StringToPermission
{
    public interface IStringToPermissionConverter : IValueConverter<string, Permission>
    {
    }
}