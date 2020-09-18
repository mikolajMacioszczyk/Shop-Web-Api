using System;
using System.Collections.Generic;
using System.Linq;
using ShopApi.DAL;
using ShopApi.Models.Furnitures;
using ShopApi.Models.Furnitures.FurnitureImplmentation;
using ShopApi.Models.Orders;
using ShopApi.Models.People;

namespace ShopApi.Tests
{
    public class ShopTestDatabaseInitializer
    {
        public static List<Collection> Collections { get; private set; }
        public static List<Chair> Chairs { get; private set; }
        public static List<Corner> Corners { get; private set; }
        public static List<Sofa> Sofas { get; private set; }
        public static List<Table> Tables { get; private set; }
        public static List<Furniture> Furnitures { get; private set; }
        public static List<FurnitureCount> FurnitureCounts { get; private set; }
        public static List<Order> Orders { get; private set; }
        public static List<Address> Addresses { get; private set; }
        public static List<Customer> Customers { get; private set; }
        public static List<Employee> Employees { get; private set; }
        public static List<Person> People { get; private set; }

        public static void Initialize(ShopDbContext context)
        {
            if (context.CollectionItems.Any())
                context.CollectionItems.RemoveRange(context.CollectionItems);
            Collections = new List<Collection>()
            {
                new Collection() {Name = "Wiosenna", IsLimited = true, IsNew = false, IsOnSale = false},
                new Collection() {Name = "Letnia", IsLimited = true, IsNew = false, IsOnSale = true},
                new Collection() {Name = "Jesienna", IsLimited = false, IsNew = true, IsOnSale = false},
                new Collection() {Name = "Zimowa", IsLimited = true, IsNew = false, IsOnSale = false},
            };
            context.CollectionItems.AddRange(Collections);
            context.SaveChanges();
            
            if (context.ChairItems.Any())
                context.ChairItems.RemoveRange(context.ChairItems);
            Chairs = new List<Chair>()
            {
                new Chair(){Collection = Collections[0], Name = "Krzesło Wiosna 1", Prize = 100, Height = 100, Length = 50, Width = 50,Weight = 20},
                new Chair(){Collection = Collections[1], Name = "Krzesło Lato 1", Prize = 100, Height = 100, Length = 50, Width = 50,Weight = 20},
                new Chair(){Collection = Collections[2], Name = "Krzesło Jesień 1", Prize = 100, Height = 100, Length = 50, Width = 50,Weight = 20},
                new Chair(){Collection = Collections[3], Name = "Krzesło Zima 1", Prize = 100, Height = 100, Length = 50, Width = 50,Weight = 20},
            };
            context.ChairItems.AddRange(Chairs);
            context.SaveChanges();
            
            if (context.CornerItems.Any())
                context.CornerItems.RemoveRange(context.CornerItems);
            Corners = new List<Corner>()
            {
                new Corner(){Collection = Collections[0], Name = "Narożnik Wiosna 1", Prize = 2500, Weight = 300, HaveHeadrests = true, HaveSleepMode = false, Height = 100, Length = 4000, Width = 100},
                new Corner(){Collection = Collections[1], Name = "Narożnik Lato 1", Prize = 2500, Weight = 300, HaveHeadrests = true, HaveSleepMode = false, Height = 100, Length = 4000, Width = 100},
                new Corner(){Collection = Collections[2], Name = "Narożnik Jesień 1", Prize = 2500, Weight = 300, HaveHeadrests = true, HaveSleepMode = false, Height = 100, Length = 4000, Width = 100},
                new Corner(){Collection = Collections[3], Name = "Narożnik Zima 1", Prize = 2500, Weight = 300, HaveHeadrests = true, HaveSleepMode = false, Height = 100, Length = 4000, Width = 100},
            };
            context.CornerItems.AddRange(Corners);
            context.SaveChanges();
            
            if (context.SofaItems.Any())
                context.SofaItems.RemoveRange(context.SofaItems);
            Sofas = new List<Sofa>()
            {
                new Sofa(){Collection = Collections[0], Name = "Sofa Wiosna 1", Prize = 1800,Height = 120, Length = 220, Width = 200, Weight = 200, Pillows = 2, HasSleepMode = true},
                new Sofa(){Collection = Collections[1], Name = "Sofa Lato 1", Prize = 1800,Height = 120, Length = 220, Width = 200, Weight = 200, Pillows = 2, HasSleepMode = true},
                new Sofa(){Collection = Collections[2], Name = "Sofa Jesień 1", Prize = 1800,Height = 120, Length = 220, Width = 200, Weight = 200, Pillows = 2, HasSleepMode = true},
                new Sofa(){Collection = Collections[3], Name = "Sofa Zima 1", Prize = 1800,Height = 120, Length = 220, Width = 200, Weight = 200, Pillows = 2, HasSleepMode = true},
            };
            context.SofaItems.AddRange(Sofas);
            context.SaveChanges();
            
            if (context.TableItems.Any())
                context.TableItems.RemoveRange(context.TableItems);
            Tables = new List<Table>()
            {
                new Table(){Collection = Collections[0], Name = "Stół Wiosna 1", Prize = 1400, Height = 80, Length = 150, Width = 120, Weight = 200, IsFoldable = true, Shape = "Rectangle"},
                new Table(){Collection = Collections[1], Name = "Stół Lato 1", Prize = 1400, Height = 80, Length = 150, Width = 120, Weight = 200, IsFoldable = true, Shape = "Rectangle"},
                new Table(){Collection = Collections[2], Name = "Stół Jesień 1", Prize = 1400, Height = 80, Length = 150, Width = 120, Weight = 200, IsFoldable = false, Shape = "Circle"},
                new Table(){Collection = Collections[3], Name = "Stół Zima 1", Prize = 1400, Height = 80, Length = 150, Width = 120, Weight = 200, IsFoldable = true, Shape = "Rectangle"},
            };
            context.TableItems.AddRange(Tables);
            context.SaveChanges();
            
            Furnitures = new List<Furniture>(Chairs);
            Furnitures.AddRange(Sofas);
            Furnitures.AddRange(Corners);
            Furnitures.AddRange(Tables);

            if (context.FurnitureCounts.Any())
                context.FurnitureCounts.RemoveRange(context.FurnitureCounts);
            FurnitureCounts = new List<FurnitureCount>()
            {
                new FurnitureCount(){FurnitureId = Chairs[0].Id, Count = 4}, 
                new FurnitureCount(){FurnitureId = Sofas[0].Id, Count = 2}, 
                new FurnitureCount(){FurnitureId = Tables[0].Id, Count = 1},
                new FurnitureCount(){FurnitureId = Corners[0].Id, Count = 1},
                new FurnitureCount(){FurnitureId = Chairs[1].Id, Count = 6}, 
                new FurnitureCount(){FurnitureId = Sofas[1].Id, Count = 1}, 
                new FurnitureCount(){FurnitureId = Tables[1].Id, Count = 1},
                new FurnitureCount(){FurnitureId = Corners[1].Id, Count = 2},
                new FurnitureCount(){FurnitureId = Chairs[2].Id, Count = 2}, 
                new FurnitureCount(){FurnitureId = Chairs[3].Id, Count = 2}, 
                new FurnitureCount(){FurnitureId = Sofas[3].Id, Count = 2}, 
                new FurnitureCount(){FurnitureId = Tables[3].Id, Count = 1},
                new FurnitureCount(){FurnitureId = Corners[3].Id, Count = 1}
            };
            context.FurnitureCounts.AddRange(FurnitureCounts);
            context.SaveChanges();
            
            if (context.OrderItems.Any())
                context.OrderItems.RemoveRange(context.OrderItems);
            Orders = new List<Order>()
            {
                new Order(){Status = Status.Accepted, Furnitures = new List<FurnitureCount>()
                {
                    FurnitureCounts[0],FurnitureCounts[1], FurnitureCounts[2],FurnitureCounts[3]
                }, DateOfAdmission = DateTime.Now, DateOfRealization = DateTime.Now.AddDays(2), TotalPrize = 4000, TotalWeight = 1000},
                new Order(){Status = Status.Rejected,Furnitures = new List<FurnitureCount>()
                {
                    FurnitureCounts[4],FurnitureCounts[5], FurnitureCounts[6],FurnitureCounts[7]
                }, DateOfAdmission = DateTime.Now, DateOfRealization = DateTime.Now.AddDays(2), TotalPrize = 4000, TotalWeight = 1000},
                new Order(){Status = Status.InRealization, Furnitures = new List<FurnitureCount>()
                {
                    FurnitureCounts[8]
                }, DateOfAdmission = DateTime.Now, DateOfRealization = DateTime.Now.AddDays(2), TotalPrize = 1000, TotalWeight = 20},
                new Order(){Status = Status.Delivered,Furnitures = new List<FurnitureCount>()
                {
                    FurnitureCounts[9],FurnitureCounts[10],FurnitureCounts[11],FurnitureCounts[12]
                }, DateOfAdmission = DateTime.Now, DateOfRealization = DateTime.Now.AddDays(2), TotalPrize = 4000, TotalWeight = 1000},
            };
            context.OrderItems.AddRange(Orders);
            context.SaveChanges();
            
            if (context.AddressItems.Any())
                context.AddressItems.AddRange(context.AddressItems);
            Addresses = new List<Address>()
            {
                new Address(){City = "Warszawa", Street = "Puławska", House = 21, PostalCode = "01-001"},
                new Address(){City = "Wrocław", Street = "Aleje Jerozolimskie", House = 15, PostalCode = "01-001"},
                new Address(){City = "Kraków", Street = "Modlińska", House = 5, PostalCode = "01-001"},
            };
            context.AddressItems.AddRange(Addresses);
            context.SaveChanges();
            
            if (context.CustomerItems.Any())
                context.CustomerItems.RemoveRange(context.CustomerItems);
            Customers = new List<Customer>()
            {
                new Customer(){Name = "Customer 1", Address = Addresses[0], Orders = new List<Order>(){Orders[0], Orders[1]}},
                new Customer(){Name = "Customer 2", Address = Addresses[1], Orders = new List<Order>(){Orders[2], Orders[3]}},
            };
            context.CustomerItems.AddRange(Customers);
            context.SaveChanges();
            
            if (context.EmployeeItems.Any())
                context.EmployeeItems.RemoveRange(context.EmployeeItems);
            Employees = new List<Employee>()
            {
                new Employee(){Name = "Employee 1", Address = Addresses[2], Salary = 4500, JobTitles = JobTitles.Seller, Permission = Permission.Write, DateOfBirth = DateTime.Now, DateOfEmployment = DateTime.Now.AddDays(2)},
                new Employee(){Name = "Employee 2", Address = Addresses[0], Salary = 8500, JobTitles = JobTitles.Administrator, Permission = Permission.WriteAndChange, DateOfBirth = DateTime.Now, DateOfEmployment = DateTime.Now.AddDays(2)},
            };
            context.EmployeeItems.AddRange(Employees);
            context.SaveChanges();
            
            People = new List<Person>(Customers);
            People.AddRange(Employees);
        }
    }
}