﻿namespace ShopApi.Models.Dtos.Orders.FurnitureCountDtos
{
    public struct FurnitureCountReadDto
    {
        public int Id { get; set; }
        public int FurnitureId { get; set; }
        public int Count { get; set; }
    }
}