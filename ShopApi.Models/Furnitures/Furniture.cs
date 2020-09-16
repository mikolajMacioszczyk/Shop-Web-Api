using System.ComponentModel.DataAnnotations;

namespace ShopApi.Models.Furnitures
{
    public abstract class Furniture
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public double Prize { get; set; }
        [Required]
        public Collection Collection { get; set; }
        [Required]
        public int Width { get; set; }
        [Required]
        public int Length { get; set; }
        [Required]
        public int Height { get; set; }
        [Required]
        public int Weight { get; set; }
        public virtual string Type { get; } = string.Empty;
    }
}