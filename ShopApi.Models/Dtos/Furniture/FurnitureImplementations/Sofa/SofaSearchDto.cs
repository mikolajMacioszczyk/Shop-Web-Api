using ShopApi.Models.Dtos.Furniture.Base;

namespace ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Sofa
{
    public class SofaSearchDto : FurnitureSearchDto
    {
        public bool? HasSleepMode { get; set; }
        public int? MinPillows { get; set; }
        public int? MaxPillows { get; set; }
    }
}