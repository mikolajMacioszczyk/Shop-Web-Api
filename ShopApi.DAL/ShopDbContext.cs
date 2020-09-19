using Microsoft.EntityFrameworkCore;
using ShopApi.Models.Furnitures;
using ShopApi.Models.Furnitures.FurnitureImplmentation;
using ShopApi.Models.Orders;
using ShopApi.Models.People;

namespace ShopApi.DAL
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
        }
        public DbSet<Collection> CollectionItems { get; set; }
        public DbSet<Furniture> FurnitureItems { get; set; }
        public DbSet<FurnitureCount> FurnitureCounts { get; set; }
        public DbSet<Chair> ChairItems { get; set; }
        public DbSet<Corner> CornerItems { get; set; }
        public DbSet<Sofa> SofaItems { get; set; }
        public DbSet<Table> TableItems { get; set; }

        public DbSet<Order> OrderItems { get; set; }

        public DbSet<Address> AddressItems { get; set; }
        public DbSet<Customer> CustomerItems { get; set; }
        public DbSet<Employee> EmployeeItems { get; set; }
        public DbSet<Person> PeopleItems { get; set; }
    }
}