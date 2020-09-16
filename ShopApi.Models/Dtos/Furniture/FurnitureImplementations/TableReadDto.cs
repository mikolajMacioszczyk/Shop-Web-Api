namespace ShopApi.Models.Dtos.Furniture.FurnitureImplementations
{
    public class TableReadDto : FurnitureReadDto
    {
        public bool IsFoldable { get; set; }
        public string Shape { get; set; }
    }
}