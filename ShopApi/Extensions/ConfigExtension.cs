using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ShopApi.DAL.Repositories.Address;
using ShopApi.DAL.Repositories.Collection;
using ShopApi.DAL.Repositories.Furniture.Base;
using ShopApi.DAL.Repositories.Furniture.Chair;
using ShopApi.DAL.Repositories.Furniture.Corner;
using ShopApi.DAL.Repositories.Furniture.Sofa;
using ShopApi.DAL.Repositories.Furniture.Table;
using ShopApi.DAL.Repositories.Orders;
using ShopApi.DAL.Repositories.People.Base;
using ShopApi.DAL.Repositories.People.Customer;
using ShopApi.DAL.Repositories.People.Emplyee;
using ShopApi.Profiles;
using ShopApi.Profiles.Converters.EnumerbleIdToEnumerableOrder;
using ShopApi.Profiles.Converters.IdToAddress;
using ShopApi.Profiles.Converters.IdToCollection;
using ShopApi.Profiles.Converters.JobTitlesToString;
using ShopApi.Profiles.Converters.PermissionToString;
using ShopApi.Profiles.Converters.StringToJobTitles;
using ShopApi.Profiles.Converters.StringToPermission;
using ShopApi.Profiles.Converters.StringToStatus;
using ShopApi.QueryBuilder.Address;
using ShopApi.QueryBuilder.Collection;
using ShopApi.QueryBuilder.Furniture;
using ShopApi.QueryBuilder.Furniture.Base;
using ShopApi.QueryBuilder.Furniture.Chair;
using ShopApi.QueryBuilder.Furniture.Corner;
using ShopApi.QueryBuilder.Furniture.Sofa;
using ShopApi.QueryBuilder.Furniture.Table;
using ShopApi.QueryBuilder.Order;
using ShopApi.QueryBuilder.People;
using ShopApi.QueryBuilder.People.Base;
using ShopApi.QueryBuilder.People.Customer;
using ShopApi.QueryBuilder.People.Employee;

namespace ShopApi.Extensions
{
    public static class ConfigExtension
    {
        public static IServiceCollection AddMapperConfig(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AddressProfile());
                mc.AddProfile(new CollectionProfile());
                mc.AddProfile(new FurnitureProfile());
                mc.AddProfile(new OrderProfile());
                mc.AddProfile(new PeopleProfile());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
        
        public static IServiceCollection AddQueryBuilderConfig(this IServiceCollection services)
        {
            return services.AddScoped<IPeopleQueryBuilder, PeopleQueryBuilder>()
                .AddScoped<IFurnitureQueryBuilder, FurnitureQueryBuilder>()
                .AddScoped<IAddressQueryBuilder, AddressQueryBuilder>()
                .AddScoped<ICollectionQueryBuilder, CollectionQueryBuilder>()
                .AddScoped<IChairQueryBuilder, ChairQueryBuilder>()
                .AddScoped<ICornerQueryBuilder, CornerQueryBuilder>()
                .AddScoped<ISofaQueryBuilder, SofaQueryBuilder>()
                .AddScoped<ITableQueryBuilder, TableQueryBuilder>()
                .AddScoped<IOrderQueryBuilder, OrderQueryBuilder>()
                .AddScoped<ICustomerQueryBuilder, CustomerQueryBuilder>()
                .AddScoped<IEmployeeQueryBuilder, EmployeeQueryBuilder>();
        }
        public static IServiceCollection AddConvertersConfig(this IServiceCollection services)
        {
            return services.AddScoped<IIdToCollectionConverter, IdToCollectionConverter>()
                .AddScoped<IStringToStatusConverter, StringToStatusConverter>()
                .AddScoped<IEnumerableIdToEnumerableOrderConverter, EnumerableIdToEnumerableOrderConverter>()
                .AddScoped<IStringToJobTitlesConverter, StringToJobTitlesConverter>()
                .AddScoped<IStringToPermissionConverter, StringToPermissionConverter>()
                .AddScoped<IJobTitlesToStringConverter, JobTitlesToStringConverter>()
                .AddScoped<IPermissionToStringConverter, PermissionToStringConverter>()
                .AddScoped<IIdToAddressConverter, IdToAddressConverter>();
        }
        
        public static IServiceCollection AddAddressesConfig(this IServiceCollection services)
        {
            return services.AddScoped<IAddressRepository, AddressRepository>();
        }
        public static IServiceCollection AddCollectionConfig(this IServiceCollection services)
        {
            return services.AddScoped<ICollectionRepository, CollectionRepository>();
        }
        public static IServiceCollection AddFurnitureConfig(this IServiceCollection services)
        {
            return services.AddScoped<ISofaRepository, SofaRepository>()
                .AddScoped<IChairRepository, ChairRepository>()
                .AddScoped<ITableRepository, TableRepository>()
                .AddScoped<ICornerRepository, CornerRepository>()
                .AddScoped<IFurnitureRepository, FurnitureRepository>();
        }

        public static IServiceCollection AddPeopleConfig(this IServiceCollection services)
        {
            return services.AddScoped<ICustomerRepository, CustomerRepository>()
                .AddScoped<IEmployeeRepository, EmployeeRepository>()
                .AddScoped<IPeopleRepository, PeopleRepository>();
        }

        public static IServiceCollection AddOrderConfig(this IServiceCollection services)
        {
            return services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}