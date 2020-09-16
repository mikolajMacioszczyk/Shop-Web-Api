using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ShopApi.Models.Furnitures;

namespace ShopApi.Models.Orders
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public double TotalPrize { get; set; }
        public int TotalWeight { get; set; }
        [Required]
        public Status Status { get; set; }
        [Required]
        public DateTime DateOfAdmission { get; set; }
        public DateTime DateOfRealization { get; set; }
        [Required]
        public IEnumerable<FurnitureCount> Furnitures { get; set; }
    }
}