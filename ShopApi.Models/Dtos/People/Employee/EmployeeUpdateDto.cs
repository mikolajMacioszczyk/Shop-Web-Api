using System;
using System.ComponentModel.DataAnnotations;

namespace ShopApi.Models.Dtos.People.Employee
{
    public class EmployeeUpdateDto : PersonUpdateDto
    {
        [Required]
        public double Salary { get; set; }
        [Required]
        public string JobTitles { get; set; }
        [Required]
        public string Permission { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public DateTime DateOfEmployment { get; set; }
    }
}