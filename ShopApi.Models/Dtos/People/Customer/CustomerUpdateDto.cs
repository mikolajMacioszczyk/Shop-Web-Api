using System.Collections.Generic;

namespace ShopApi.Models.Dtos.People.Customer
{
    public class CustomerUpdateDto : PersonUpdateDto
    {
        public IEnumerable<int> OrderIds { get; set; }
    }
}