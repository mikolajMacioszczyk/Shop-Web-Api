namespace ShopApi.Models.Dtos.Furniture
{
    public class CollectionReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsNew { get; set; }
        public bool IsLimited { get; set; }
        public bool IsOnSale { get; set; }
    }
}