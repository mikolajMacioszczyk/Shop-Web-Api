using AutoMapper;
using ShopApi.Models.People;

namespace ShopApi.Profiles.Converters.JobTitlesToString
{
    public class JobTitlesToStringConverter : IJobTitlesToStringConverter
    {
        public string Convert(JobTitles sourceMember, ResolutionContext context)
        {
            return sourceMember.ToString();
        }
    }
}