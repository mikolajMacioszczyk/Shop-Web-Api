using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ShopApi.Models.Dtos.Orders.FurnitureCountDtos;

namespace ShopApi.Models.Dtos.Orders.OrderDtos
{
    public class OrderUpdateDto
    {
        [Required]
        [Range(0.1,Double.MaxValue)]
        public double TotalPrize { get; set; }
        [Required]
        [Range(1,Int32.MaxValue)]
        public int TotalWeight { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime DateOfAdmission { get; set; }
        public DateTime DateOfRealization { get; set; }
        public IEnumerable<FurnitureCountCreateDto> Furnitures { get; set; }
    }
}