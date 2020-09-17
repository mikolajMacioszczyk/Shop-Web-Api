namespace ShopApi.Models.Dtos.Furniture
{
    public class FurnitureCreateDto
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public double Prize { get; set; }
        public int CollectionId { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
    }
}