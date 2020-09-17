using System;
using System.Collections.Generic;
using ShopApi.Models.Orders;

namespace ShopApi.Models.Dtos.Orders.OrderDtos
{
    public class OrderReadDto
    {
        public int Id { get; set; }
        public double TotalPrize { get; set; }
        public int TotalWeight { get; set; }
        public Status Status { get; set; }
        public DateTime DateOfAdmission { get; set; }
        public DateTime DateOfRealization { get; set; }
        public IEnumerable<FurnitureCount> Furnitures { get; set; }
    }
}