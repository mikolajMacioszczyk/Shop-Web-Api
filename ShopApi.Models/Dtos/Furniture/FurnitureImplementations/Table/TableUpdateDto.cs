using System.ComponentModel.DataAnnotations;
using ShopApi.Models.Dtos.Furniture.Base;

namespace ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Table
{
    public class TableUpdateDto : FurnitureUpdateDto
    {
        [Required]
        public bool IsFoldable { get; set; }
        [Required]
        public string Shape { get; set; }
    }
}