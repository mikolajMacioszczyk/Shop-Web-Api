using System.Collections.Generic;
using ShopApi.Models.Dtos.Orders;
using ShopApi.Models.Dtos.Orders.OrderDtos;
using ShopApi.Models.Dtos.People.Base;

namespace ShopApi.Models.Dtos.People.Customer
{
    public class CustomerReadDto : PersonReadDto
    {
        public IEnumerable<OrderReadDto> Orders { get; set; }
    }
}