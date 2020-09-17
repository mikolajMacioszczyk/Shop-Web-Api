using System;
using AutoMapper;
using ShopApi.Models.People;

namespace ShopApi.Profiles.Converters.StringToJobTitles
{
    public class StringToJobTitlesConverter : IStringToJobTitlesConverter
    {
        public JobTitles Convert(string sourceMember, ResolutionContext context)
        {
            try
            {
                return (JobTitles) Enum.Parse(typeof(JobTitles), sourceMember);
            }
            catch (ArgumentException)
            {
                return JobTitles.Seller;
            }
        }
    }
}