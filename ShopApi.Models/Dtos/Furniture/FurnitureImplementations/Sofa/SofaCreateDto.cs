﻿using System;
using System.ComponentModel.DataAnnotations;
using ShopApi.Models.Dtos.Furniture.Base;

namespace ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Sofa
{
    public class SofaCreateDto : FurnitureCreateDto
    {
        [Required]
        public bool HasSleepMode { get; set; }
        [Required]
        [Range(0, Int32.MaxValue)]
        public int Pillows { get; set; }
    }
}