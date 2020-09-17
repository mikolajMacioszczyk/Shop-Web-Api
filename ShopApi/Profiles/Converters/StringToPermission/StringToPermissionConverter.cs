using System;
using AutoMapper;
using ShopApi.Models.People;

namespace ShopApi.Profiles.Converters.StringToPermission
{ 
    public class StringToPermissionConverter : IStringToPermissionConverter
    {
        public Permission Convert(string sourceMember, ResolutionContext context)
        {
            try
            {
                return (Permission) Enum.Parse(typeof(Permission), sourceMember);
            }
            catch (ArgumentException)
            {
                return Permission.Read;
            }
        }
    }
}