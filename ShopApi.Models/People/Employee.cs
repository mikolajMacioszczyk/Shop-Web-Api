using System;
using System.ComponentModel.DataAnnotations;

namespace ShopApi.Models.People
{
    public class Employee : Person
    {
        [Required]
        public double Salary { get; set; }
        [Required]
        public JobTitles JobTitles { get; set; }
        [Required]
        public Permission Permission { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfEmployment { get; set; }
    }
}