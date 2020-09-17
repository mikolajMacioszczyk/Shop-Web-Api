using ShopApi.Models.Dtos.Furniture.Base;

namespace ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Corner
{
    public class CornerReadDto : FurnitureReadDto
    {
        public bool HaveSleepMode { get; set; }
        public bool HaveHeadrests { get; set; }
    }
}