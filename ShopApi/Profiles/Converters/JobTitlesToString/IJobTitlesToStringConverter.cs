using AutoMapper;
using ShopApi.Models.People;

namespace ShopApi.Profiles.Converters.JobTitlesToString
{
    public interface IJobTitlesToStringConverter : IValueConverter<JobTitles, string>
    {
    }
}