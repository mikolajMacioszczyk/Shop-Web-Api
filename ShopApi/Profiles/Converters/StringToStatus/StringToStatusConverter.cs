using System;
using AutoMapper;
using ShopApi.Models.Orders;

namespace ShopApi.Profiles.Converters.StringToStatus
{
    public class StringToStatusConverter : IStringToStatusConverter
    {
        public Status Convert(string source, ResolutionContext context)
        {
            Status output;
            try
            {
                output = (Status) Enum.Parse(typeof(Status), source);
            }
            catch (ArgumentException)
            {
                output = Status.Rejected;
            }
            return output;
        }
    }
}