using ShopApi.Models.Dtos.Address;

namespace ShopApi.Models.Dtos.People.Base
{
    public class PersonReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AddressReadDto Address { get; set; }
    }
}