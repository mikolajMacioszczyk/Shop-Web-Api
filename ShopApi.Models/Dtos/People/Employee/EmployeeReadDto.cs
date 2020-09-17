using System;
using ShopApi.Models.Dtos.People.Base;
using ShopApi.Models.People;

namespace ShopApi.Models.Dtos.People.Employee
{
    public class EmployeeReadDto : PersonReadDto
    {
        public double Salary { get; set; }
        public string JobTitles { get; set; }
        public string Permission { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfEmployment { get; set; }
    }
}