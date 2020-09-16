namespace ShopApi.Models.Dtos.People
{
    public class PersonReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AddressReadDto Address { get; set; }
    }
}