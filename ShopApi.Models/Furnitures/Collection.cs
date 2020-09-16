using System.ComponentModel.DataAnnotations;

namespace ShopApi.Models.Furnitures
{
    public class Collection
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public bool IsNew { get; set; }
        public bool IsLimited { get; set; }
        public bool IsOnSale { get; set; }
    }
}