namespace ShopApi.Models.Dtos.Collection
{
    public class CollectionSearchDto
    {
        public string Name { get; set; }
        public bool? IsNew { get; set; }
        public bool? IsLimited { get; set; }
        public bool? IsOnSale { get; set; }
    }
}