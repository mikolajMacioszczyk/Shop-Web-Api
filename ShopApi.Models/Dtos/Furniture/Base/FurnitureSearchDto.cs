namespace ShopApi.Models.Dtos.Furniture.Base
{
    public class FurnitureSearchDto
    {
        public string Name { get; set; }
        public double? MinPrize { get; set; }
        public double? MaxPrize { get; set; }
        public int? CollectionId { get; set; }
        public int? MinWidth { get; set; }
        public int? MaxWidth { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public int? MinHeight { get; set; }
        public int? MaxHeight { get; set; }
        public int? MinWeight { get; set; }
        public int? MaxWeight { get; set; }
    }
}