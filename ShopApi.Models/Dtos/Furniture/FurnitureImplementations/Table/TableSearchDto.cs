using ShopApi.Models.Dtos.Furniture.Base;

namespace ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Table
{
    public class TableSearchDto : FurnitureSearchDto
    {
        public bool? IsFoldable { get; set; }
        public string Shape { get; set; }
    }
}