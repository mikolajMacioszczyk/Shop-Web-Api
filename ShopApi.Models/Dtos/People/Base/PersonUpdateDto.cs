using System.ComponentModel.DataAnnotations;

namespace ShopApi.Models.Dtos.People.Base
{
    public class PersonUpdateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int AddressId { get; set; }
    }
}