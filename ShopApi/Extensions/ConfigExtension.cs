using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopApi.DAL.Repositories.Furniture.Base;
using ShopApi.DAL.Repositories.Furniture.Chair;
using ShopApi.DAL.Repositories.Furniture.Corner;
using ShopApi.DAL.Repositories.Furniture.Sofa;
using ShopApi.DAL.Repositories.Furniture.Table;
using ShopApi.DAL.Repositories.Orders;
using ShopApi.DAL.Repositories.People.Customer;
using ShopApi.DAL.Repositories.People.Emplyee;

namespace ShopApi.Extensions
{
    public static class ConfigExtension
    {
        public static IServiceCollection AddFurnitureConfig(this IServiceCollection services)
        {
            services.AddScoped<ISofaRepository, SofaRepository>()
                .AddScoped<IChairRepository, ChairRepository>()
                .AddScoped<ITableRepository, TableRepository>()
                .AddScoped<ICornerRepository, CornerRepository>()
                .AddScoped<IFurnitureRepository, FurnitureRepository>();
            return services;
        }

        public static IServiceCollection AddPeopleConfig(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>()
                .AddScoped<IEmployeeRepository, EmployeeRepository>();
            return services;
        }

        public static IServiceCollection AddOrderConfig(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}