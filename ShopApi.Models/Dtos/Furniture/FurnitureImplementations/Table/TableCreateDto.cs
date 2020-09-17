using System.ComponentModel.DataAnnotations;

namespace ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Table
{
    public class TableCreateDto : FurnitureCreateDto
    {
        [Required]
        public bool IsFoldable { get; set; }
        [Required]
        public string Shape { get; set; }
    }
}