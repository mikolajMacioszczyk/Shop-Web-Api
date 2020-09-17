using System.ComponentModel.DataAnnotations;

namespace ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Corner
{
    public class CornerUpdateDto : FurnitureUpdateDto
    {
        [Required]
        public bool HaveSleepMode { get; set; }
        [Required]
        public bool HaveHeadrests { get; set; }
    }
}