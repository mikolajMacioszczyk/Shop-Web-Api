using System;
using System.ComponentModel.DataAnnotations;

namespace ShopApi.Models.Dtos.Orders
{
    public class FurnitureCountCreateDto
    {
        [Required]
        public int FurnitureId { get; set; }
        [Required]
        [Range(1,Int32.MaxValue)]
        public int Count { get; set; }
    }
}