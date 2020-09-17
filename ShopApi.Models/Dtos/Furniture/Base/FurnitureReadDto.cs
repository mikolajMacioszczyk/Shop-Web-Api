using ShopApi.Models.Dtos.Collection;

namespace ShopApi.Models.Dtos.Furniture.Base
{
    public class FurnitureReadDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public double Prize { get; set; }
        public CollectionReadDto Collection { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
    }
}