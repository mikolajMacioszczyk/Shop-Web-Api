using ShopApi.Models.Dtos.People.Base;

namespace ShopApi.Models.Dtos.People.Customer
{
    public class CustomerSearchDto : PersonSearchDto
    {
        public int[] OrderIds { get; set; }
    }
}