using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApi.Models.Furnitures;
using ShopApi.Models.Furnitures.FurnitureImplmentation;
using ShopApi.Models.Orders;
using ShopApi.Models.People;

namespace ShopApi.DAL
{
    public class DataSeeder
    {
        public static async Task InitializeAsync(ShopDbContext context)
        {
            List<Collection> collections;
            List<Chair> chairs;
            List<Corner> corners;
            List<Sofa> sofas;
            List<Table> tables;
            List<Order> orders;
            List<Address> addresses;
            List<FurnitureCount> furnitureCounts;
            if (!context.CollectionItems.Any())
            {
                collections = new List<Collection>()
                {
                    new Collection(){Name = "Wiosenna", IsLimited = true, IsNew = false, IsOnSale = false},
                    new Collection(){Name = "Letnia", IsLimited = true, IsNew = false, IsOnSale = true},
                    new Collection(){Name = "Jesienna", IsLimited = false, IsNew = true, IsOnSale = false},
                    new Collection(){Name = "Zimowa", IsLimited = true, IsNew = false, IsOnSale = false},
                };
                await context.CollectionItems.AddRangeAsync(collections);
                await context.SaveChangesAsync();
            }
            else
            {
                collections = await context.CollectionItems.ToListAsync();
            }
            if (!context.ChairItems.Any())
            {
                chairs = new List<Chair>()
                {
                    new Chair(){Collection = collections[0], Name = "Krzesło Wiosna 1", Prize = 100, Height = 100, Length = 50, Width = 50,Weight = 20},
                    new Chair(){Collection = collections[1], Name = "Krzesło Lato 1", Prize = 100, Height = 100, Length = 50, Width = 50,Weight = 20},
                    new Chair(){Collection = collections[2], Name = "Krzesło Jesień 1", Prize = 100, Height = 100, Length = 50, Width = 50,Weight = 20},
                    new Chair(){Collection = collections[3], Name = "Krzesło Zima 1", Prize = 100, Height = 100, Length = 50, Width = 50,Weight = 20},
                };
                await context.ChairItems.AddRangeAsync(chairs);
                await context.SaveChangesAsync();
            }
            else
            {
                chairs = await context.ChairItems.ToListAsync();
            }
            if (!context.CornerItems.Any())
            {
                corners = new List<Corner>()
                {
                    new Corner(){Collection = collections[0], Name = "Narożnik Wiosna 1", Prize = 2500, Weight = 300, HaveHeadrests = true, HaveSleepMode = false, Height = 100, Length = 4000, Width = 100},
                    new Corner(){Collection = collections[1], Name = "Narożnik Lato 1", Prize = 2500, Weight = 300, HaveHeadrests = true, HaveSleepMode = false, Height = 100, Length = 4000, Width = 100},
                    new Corner(){Collection = collections[2], Name = "Narożnik Jesień 1", Prize = 2500, Weight = 300, HaveHeadrests = true, HaveSleepMode = false, Height = 100, Length = 4000, Width = 100},
                    new Corner(){Collection = collections[3], Name = "Narożnik Zima 1", Prize = 2500, Weight = 300, HaveHeadrests = true, HaveSleepMode = false, Height = 100, Length = 4000, Width = 100},
                };
                await context.CornerItems.AddRangeAsync(corners);
                await context.SaveChangesAsync();
            }
            else
            {
                corners = await context.CornerItems.ToListAsync();
            }
            if (!context.SofaItems.Any())
            {
                sofas = new List<Sofa>()
                {
                    new Sofa(){Collection = collections[0], Name = "Sofa Wiosna 1", Prize = 1800,Height = 120, Length = 220, Width = 200, Weight = 200, Pillows = 2, HasSleepMode = true},
                    new Sofa(){Collection = collections[1], Name = "Sofa Lato 1", Prize = 1800,Height = 120, Length = 220, Width = 200, Weight = 200, Pillows = 2, HasSleepMode = true},
                    new Sofa(){Collection = collections[2], Name = "Sofa Jesień 1", Prize = 1800,Height = 120, Length = 220, Width = 200, Weight = 200, Pillows = 2, HasSleepMode = true},
                    new Sofa(){Collection = collections[3], Name = "Sofa Zima 1", Prize = 1800,Height = 120, Length = 220, Width = 200, Weight = 200, Pillows = 2, HasSleepMode = true},
                };
                await context.SofaItems.AddRangeAsync(sofas);
                await context.SaveChangesAsync();
            }
            else
            {
                sofas = await context.SofaItems.ToListAsync();
            }
            if (!context.TableItems.Any())
            {
                tables = new List<Table>()
                {
                    new Table(){Collection = collections[0], Name = "Stół Wiosna 1", Prize = 1400, Height = 80, Length = 150, Width = 120, Weight = 200, IsFoldable = true, Shape = "Rectangle"},
                    new Table(){Collection = collections[1], Name = "Stół Lato 1", Prize = 1400, Height = 80, Length = 150, Width = 120, Weight = 200, IsFoldable = true, Shape = "Rectangle"},
                    new Table(){Collection = collections[2], Name = "Stół Jesień 1", Prize = 1400, Height = 80, Length = 150, Width = 120, Weight = 200, IsFoldable = false, Shape = "Circle"},
                    new Table(){Collection = collections[3], Name = "Stół Zima 1", Prize = 1400, Height = 80, Length = 150, Width = 120, Weight = 200, IsFoldable = true, Shape = "Rectangle"},
                };
                await context.TableItems.AddRangeAsync(tables);
                await context.SaveChangesAsync();
            }
            else
            {
                tables = await context.TableItems.ToListAsync();
            }
            if (!context.FurnitureCounts.Any())
            {
                furnitureCounts = new List<FurnitureCount>()
                {
                    new FurnitureCount(){FurnitureId = chairs[0].Id, Count = 4}, 
                    new FurnitureCount(){FurnitureId = sofas[0].Id, Count = 2}, 
                    new FurnitureCount(){FurnitureId = tables[0].Id, Count = 1},
                    new FurnitureCount(){FurnitureId = corners[0].Id, Count = 1},
                    new FurnitureCount(){FurnitureId = chairs[1].Id, Count = 6}, 
                    new FurnitureCount(){FurnitureId = sofas[1].Id, Count = 1}, 
                    new FurnitureCount(){FurnitureId = tables[1].Id, Count = 1},
                    new FurnitureCount(){FurnitureId = collections[1].Id, Count = 2},
                    new FurnitureCount(){FurnitureId = chairs[2].Id, Count = 2}, 
                    new FurnitureCount(){FurnitureId = chairs[3].Id, Count = 2}, 
                    new FurnitureCount(){FurnitureId = sofas[3].Id, Count = 2}, 
                    new FurnitureCount(){FurnitureId = tables[3].Id, Count = 1},
                    new FurnitureCount(){FurnitureId = corners[3].Id, Count = 1}
                };
                await context.FurnitureCounts.AddRangeAsync(furnitureCounts);
                await context.SaveChangesAsync();
            }
            else
            {
                furnitureCounts = await context.FurnitureCounts.ToListAsync();
            }
            if (!context.OrderItems.Any())
            {
                orders = new List<Order>()
                {
                    new Order(){Status = Status.Accepted, Furnitures = new FurnitureCount[]
                    {
                        furnitureCounts[0],furnitureCounts[1], furnitureCounts[2],furnitureCounts[3]
                    }, DateOfAdmission = DateTime.Now, DateOfRealization = DateTime.Now.AddDays(2), TotalPrize = 4000, TotalWeight = 1000},
                    new Order(){Status = Status.Rejected,Furnitures = new FurnitureCount[]
                    {
                        furnitureCounts[4],furnitureCounts[5], furnitureCounts[6],furnitureCounts[7]
                    }, DateOfAdmission = DateTime.Now, DateOfRealization = DateTime.Now.AddDays(2), TotalPrize = 4000, TotalWeight = 1000},
                    new Order(){Status = Status.InRealization, Furnitures = new FurnitureCount[]
                    {
                        furnitureCounts[8]
                    }, DateOfAdmission = DateTime.Now, DateOfRealization = DateTime.Now.AddDays(2), TotalPrize = 1000, TotalWeight = 20},
                    new Order(){Status = Status.Delivered,Furnitures = new FurnitureCount[]
                    {
                        furnitureCounts[9],furnitureCounts[10],furnitureCounts[11],furnitureCounts[12]
                    }, DateOfAdmission = DateTime.Now, DateOfRealization = DateTime.Now.AddDays(2), TotalPrize = 4000, TotalWeight = 1000},
                };
                await context.OrderItems.AddRangeAsync(orders);
                await context.SaveChangesAsync();
            }
            else
            {
                orders = await context.OrderItems.ToListAsync();
            }
            if (!context.AddressItems.Any())
            {
                addresses = new List<Address>()
                {
                    new Address(){City = "Warszawa", Street = "Puławska", House = 21, PostalCode = "01-001"},
                    new Address(){City = "Warszawa", Street = "Aleje Jerozolimskie", House = 15, PostalCode = "01-001"},
                    new Address(){City = "Warszawa", Street = "Modlińska", House = 5, PostalCode = "01-001"},
                };
                await context.AddressItems.AddRangeAsync(addresses);
                await context.SaveChangesAsync();
            }
            else
            {
                addresses = await context.AddressItems.ToListAsync();
            }
            if (!context.CustomerItems.Any())
            {
                var customers = new List<Customer>()
                {
                    new Customer(){Name = "Customer 1", Address = addresses[0], Orders = new Order[]{orders[0], orders[1]}},
                    new Customer(){Name = "Customer 2", Address = addresses[1], Orders = new Order[]{orders[2], orders[3]}},
                };
                await context.CustomerItems.AddRangeAsync(customers);
                await context.SaveChangesAsync();
            }

            if (!context.EmployeeItems.Any())
            {
                var employees = new List<Employee>()
                {
                    new Employee(){Name = "Employee 1", Address = addresses[2], Salary = 4500, JobTitles = JobTitles.Seller, Permission = Permission.Write, DateOfBirth = DateTime.Now, DateOfEmployment = DateTime.Now.AddDays(2)},
                    new Employee(){Name = "Employee 2", Address = addresses[0], Salary = 8500, JobTitles = JobTitles.Administrator, Permission = Permission.WriteAndChange, DateOfBirth = DateTime.Now, DateOfEmployment = DateTime.Now.AddDays(2)},
                };
                await context.EmployeeItems.AddRangeAsync(employees);
                await context.SaveChangesAsync();
            }
        }
    }
}