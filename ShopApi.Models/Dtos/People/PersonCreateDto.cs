﻿using System.ComponentModel.DataAnnotations;

namespace ShopApi.Models.Dtos.People
{
    public class PersonCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int AddressId { get; set; }
    }
}