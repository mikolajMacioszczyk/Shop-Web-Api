using ShopApi.Models.Dtos.Furniture;

namespace ShopApi.Models.Dtos.Orders
{
    public struct FurnitureCountReadDto
    {
        public int FurnitureId { get; set; }
        public FurnitureReadDto Furniture { get; set; }
        public int Count { get; set; }
    }
}