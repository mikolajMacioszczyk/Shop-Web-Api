using System.ComponentModel.DataAnnotations;

namespace ShopApi.Models.Furnitures.FurnitureImplmentation
{
    public class Table : Furniture
    {
        public override string Type { get; } = "Table";
        [Required]
        public bool IsFoldable { get; set; }
        public string Shape { get; set; }
    }
}