using System.Collections.Generic;
using ShopApi.Models.Orders;

namespace ShopApi.Models.People
{
    public class Customer : Person
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}