using System.Collections.Generic;
using ShopApi.Models.Dtos.Orders;

namespace ShopApi.Models.Dtos.People.Customer
{
    public class CustomerReadDto : PersonReadDto
    {
        public IEnumerable<OrderReadDto> Orders { get; set; }
    }
}