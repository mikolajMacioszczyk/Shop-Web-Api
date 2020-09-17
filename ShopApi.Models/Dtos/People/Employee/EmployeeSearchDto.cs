using System;
using ShopApi.Models.Dtos.People.Base;

namespace ShopApi.Models.Dtos.People.Employee
{
    public class EmployeeSearchDto : PersonSearchDto
    {
        public double? MinSalary { get; set; }
        public double? MaxSalary { get; set; }
        public string JobTitles { get; set; }
        public string Permission { get; set; }
        public DateTime? MinDateOfBirth { get; set; }
        public DateTime? MaxDateOfBirth { get; set; }
        public DateTime? MinDateOfEmployment { get; set; }
        public DateTime? MaxDateOfEmployment { get; set; }
    }
}