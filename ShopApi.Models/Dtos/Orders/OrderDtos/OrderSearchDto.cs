using System;
using System.Collections.Generic;
using ShopApi.Models.Orders;

namespace ShopApi.Models.Dtos.Orders.OrderDtos
{
    public class OrderSearchDto
    {
        public double? MinTotalPrize { get; set; }
        public double? MaxTotalPrize { get; set; }
        public int? MinTotalWeight { get; set; }
        public int? MaxTotalWeight { get; set; }
        public string Status { get; set; }
        public DateTime? MinDateOfAdmission { get; set; }
        public DateTime? MaxDateOfAdmission { get; set; }
        public DateTime? MinDateOfRealization { get; set; }
        public DateTime? MaxDateOfRealization { get; set; }
        public int[] FurnitureIds { get; set; }
    }
}