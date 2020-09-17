using AutoMapper;
using ShopApi.Models.People;

namespace ShopApi.Profiles.Converters.StringToJobTitles
{
    public interface IStringToJobTitlesConverter : IValueConverter<string, JobTitles>
    {
    }
}