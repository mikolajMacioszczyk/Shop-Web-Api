namespace ShopApi.Models.Dtos.Address
{
    public class AddressReadDto
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int House { get; set; }
        public string PostalCode { get; set; }
    }
}