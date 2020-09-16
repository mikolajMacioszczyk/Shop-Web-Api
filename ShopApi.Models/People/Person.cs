using System.ComponentModel.DataAnnotations;

namespace ShopApi.Models.People
{
    public abstract class Person
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public Address Address { get; set; }
    }
}