﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ShopApi.Models.Dtos.Address
{
    public class AddressUpdateDto
    {
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        [Range(1,Int32.MaxValue)]
        public int House { get; set; }
        [Required]
        public string PostalCode { get; set; }
    }
}