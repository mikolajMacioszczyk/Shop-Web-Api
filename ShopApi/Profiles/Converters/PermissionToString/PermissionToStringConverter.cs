using AutoMapper;
using ShopApi.Models.People;

namespace ShopApi.Profiles.Converters.PermissionToString
{
    public class PermissionToStringConverter : IPermissionToStringConverter
    {
        public string Convert(Permission sourceMember, ResolutionContext context)
        {
            return sourceMember.ToString();
        }
    }
}