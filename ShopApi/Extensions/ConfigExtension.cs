using Microsoft.Extensions.DependencyInjection;
using ShopApi.DAL.Repositories.Address;
using ShopApi.DAL.Repositories.Collection;
using ShopApi.DAL.Repositories.Furniture.Base;
using ShopApi.DAL.Repositories.Furniture.Chair;
using ShopApi.DAL.Repositories.Furniture.Corner;
using ShopApi.DAL.Repositories.Furniture.Sofa;
using ShopApi.DAL.Repositories.Furniture.Table;
using ShopApi.DAL.Repositories.Orders;
using ShopApi.DAL.Repositories.People.Customer;
using ShopApi.DAL.Repositories.People.Emplyee;
using ShopApi.Profiles.Converters.EnumerbleIdToEnumerableOrder;
using ShopApi.Profiles.Converters.IdToAddress;
using ShopApi.Profiles.Converters.IdToCollection;
using ShopApi.Profiles.Converters.JobTitlesToString;
using ShopApi.Profiles.Converters.PermissionToString;
using ShopApi.Profiles.Converters.StringToJobTitles;
using ShopApi.Profiles.Converters.StringToPermission;
using ShopApi.Profiles.Converters.StringToStatus;

namespace ShopApi.Extensions
{
    public static class ConfigExtension
    {
        public static IServiceCollection AddConvertersConfig(this IServiceCollection services)
        {
            services.AddScoped<IIdToCollectionConverter, IdToCollectionConverter>()
                .AddScoped<IStringToStatusConverter, StringToStatusConverter>()
                .AddScoped<IEnumerableIdToEnumerableOrderConverter, EnumerableIdToEnumerableOrderConverter>()
                .AddScoped<IStringToJobTitlesConverter, StringToJobTitlesConverter>()
                .AddScoped<IStringToPermissionConverter, StringToPermissionConverter>()
                .AddScoped<IJobTitlesToStringConverter, JobTitlesToStringConverter>()
                .AddScoped<IPermissionToStringConverter, PermissionToStringConverter>()
                .AddScoped<IIdToAddressConverter, IdToAddressConverter>();
            return services;
        }
        
        public static IServiceCollection AddAddressesConfig(this IServiceCollection services)
        {
            services.AddScoped<IAddressRepository, AddressRepository>();
            return services;
        }
        public static IServiceCollection AddCollectionConfig(this IServiceCollection services)
        {
            services.AddScoped<ICollectionRepository, CollectionRepository>();
            return services;
        }
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