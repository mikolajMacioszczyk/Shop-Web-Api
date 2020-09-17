﻿using System.Collections.Generic;
using ShopApi.Models.Dtos.People.Base;

namespace ShopApi.Models.Dtos.People.Customer
{
    public class CustomerCreateDto : PersonCreateDto
    {
        public IEnumerable<int> OrderIds { get; set; }
    }
}