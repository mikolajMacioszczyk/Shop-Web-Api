﻿namespace ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Table
{
    public class TableReadDto : FurnitureReadDto
    {
        public bool IsFoldable { get; set; }
        public string Shape { get; set; }
    }
}