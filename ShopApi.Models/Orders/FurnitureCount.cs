using ShopApi.Models.Furnitures;

namespace ShopApi.Models.Orders
{
    public class FurnitureCount
    {
        public Furniture Furniture { get; set; }
        public int FurnitureId { get; set; }
        public int Count { get; set; }
    }
}