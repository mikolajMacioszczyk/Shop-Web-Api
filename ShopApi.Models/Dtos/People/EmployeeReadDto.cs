using System;
using ShopApi.Models.People;

namespace ShopApi.Models.Dtos.People
{
    public class EmployeeReadDto : PersonReadDto
    {
        public double Salary { get; set; }
        public JobTitles JobTitles { get; set; }
        public Permission Permission { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfEmployment { get; set; }
    }
}