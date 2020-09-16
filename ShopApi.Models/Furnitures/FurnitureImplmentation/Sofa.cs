using System.ComponentModel.DataAnnotations;

namespace ShopApi.Models.Furnitures.FurnitureImplmentation
{
    public class Sofa : Furniture
    {
        public override string Type { get; } = "Sofa";
        [Required]
        public bool HasSleepMode { get; set; }
        public int Pillows { get; set; }
    }
}