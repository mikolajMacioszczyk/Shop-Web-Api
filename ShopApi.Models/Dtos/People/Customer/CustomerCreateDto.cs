using System.Collections.Generic;

namespace ShopApi.Models.Dtos.People.Customer
{
    public class CustomerCreateDto : PersonCreateDto
    {
        public IEnumerable<int> OrderIds { get; set; }
    }
}