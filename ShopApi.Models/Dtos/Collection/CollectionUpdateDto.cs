using System.ComponentModel.DataAnnotations;

namespace ShopApi.Models.Dtos.Collection
{
    public class CollectionUpdateDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public bool IsNew { get; set; }
        [Required]
        public bool IsLimited { get; set; }
        [Required]
        public bool IsOnSale { get; set; }
    }
}