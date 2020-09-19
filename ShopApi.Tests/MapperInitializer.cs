using AutoMapper;
using ShopApi.DAL;
using ShopApi.Models.Dtos.Address;
using ShopApi.Models.Dtos.Collection;
using ShopApi.Models.Dtos.Furniture.Base;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Chair;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Corner;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Sofa;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Table;
using ShopApi.Models.Dtos.Orders.FurnitureCountDtos;
using ShopApi.Models.Dtos.Orders.OrderDtos;
using ShopApi.Models.Dtos.People.Base;
using ShopApi.Models.Dtos.People.Customer;
using ShopApi.Models.Dtos.People.Employee;
using ShopApi.Models.Furnitures;
using ShopApi.Models.Furnitures.FurnitureImplmentation;
using ShopApi.Models.Orders;
using ShopApi.Models.People;
using ShopApi.Profiles.Converters.EnumerbleIdToEnumerableOrder;
using ShopApi.Profiles.Converters.IdToAddress;
using ShopApi.Profiles.Converters.IdToCollection;
using ShopApi.Profiles.Converters.JobTitlesToString;
using ShopApi.Profiles.Converters.PermissionToString;
using ShopApi.Profiles.Converters.StringToJobTitles;
using ShopApi.Profiles.Converters.StringToPermission;
using ShopApi.Profiles.Converters.StringToStatus;

namespace ShopApi.Tests
{
    public static class MapperInitializer
    {
        public static IMapper GetMapper(ShopDbContext context)
        {
            var config = new MapperConfiguration(cfg =>
                {
                    // Model => ReadDto
                    cfg.CreateMap<Chair, ChairReadDto>();
                    cfg.CreateMap<Collection, CollectionReadDto>();
                    cfg.CreateMap<Corner, CornerReadDto>();
                    cfg.CreateMap<Sofa, SofaReadDto>();
                    cfg.CreateMap<Table, TableReadDto>();
                    cfg.CreateMap<Furniture, FurnitureReadDto>();
                    cfg.CreateMap<FurnitureCount, FurnitureCountReadDto>();
                    cfg.CreateMap<Collection, CollectionReadDto>();

                    cfg.CreateMap<Order, OrderReadDto>()
                        .ForMember(target => target.Furnitures,
                            obj => obj.MapFrom(src => src.Furnitures));
                    
                    cfg.CreateMap<Address, AddressReadDto>();
                    cfg.CreateMap<Person, PersonReadDto>();
                    cfg.CreateMap<Customer, CustomerReadDto>();
                    cfg.CreateMap<Employee, EmployeeReadDto>()
                        .ForMember(target => target.JobTitles,
                            obt =>
                                obt.ConvertUsing(new JobTitlesToStringConverter()))
                        .ForMember(target => target.Permission,
                            obt =>
                                obt.ConvertUsing(new PermissionToStringConverter()));
                    
                    
                    // CreateDto => Model
                    cfg.CreateMap<CollectionCreateDto, Collection>();
                    cfg.CreateMap<ChairCreateDto, Chair>()
                        .ForMember(target => target.Collection,
                            obt => 
                                obt.ConvertUsing(new IdToCollectionConverter(context),src => src.CollectionId));
                    cfg.CreateMap<CornerCreateDto, Corner>()
                        .ForMember(target => target.Collection,
                            obt => 
                                obt.ConvertUsing(new IdToCollectionConverter(context),src => src.CollectionId));
                    cfg.CreateMap<SofaCreateDto, Sofa>()
                        .ForMember(target => target.Collection,
                            obt => 
                                obt.ConvertUsing(new IdToCollectionConverter(context),src => src.CollectionId));
                    cfg.CreateMap<TableCreateDto, Table>()
                        .ForMember(target => target.Collection,
                            obt => 
                                obt.ConvertUsing(new IdToCollectionConverter(context),src => src.CollectionId));
                    cfg.CreateMap<FurnitureCountCreateDto, FurnitureCount>();
                    cfg.CreateMap<OrderCreateDto, Order>()
                        .ForMember(target => target.Status,
                            obt => 
                                obt.ConvertUsing(new StringToStatusConverter()));
                    cfg.CreateMap<AddressCreateDto, Address>();
                    cfg.CreateMap<CustomerCreateDto, Customer>()
                        .ForMember(target => target.Orders,
                            obt =>
                                obt.ConvertUsing(new EnumerableIdToEnumerableOrderConverter(context),
                                    src => src.OrderIds))
                        .ForMember(target => target.Address,
                            obt =>
                                obt.ConvertUsing(new IdToAddressConverter(context), src => src.AddressId));
                    cfg.CreateMap<EmployeeCreateDto, Employee>()
                        .ForMember(target => target.JobTitles,
                            obt =>
                                obt.ConvertUsing(new StringToJobTitlesConverter()))
                        .ForMember(target => target.Permission,
                            obt =>
                                obt.ConvertUsing(new StringToPermissionConverter()))
                        .ForMember(target => target.Address,
                            obt =>
                                obt.ConvertUsing(new IdToAddressConverter(context), src => src.AddressId));
                    
                    // Update => Model
                    cfg.CreateMap<CollectionUpdateDto, Collection>();
                    cfg.CreateMap<FurnitureCountUpdateDto, FurnitureCount>();
                    cfg.CreateMap<OrderUpdateDto, Order>()
                        .ForMember(target => target.Status,
                            obt => obt.ConvertUsing(new StringToStatusConverter()));
                    cfg.CreateMap<AddressUpdateDto, Address>();
                    cfg.CreateMap<CharUpdateDto, Chair>()
                        .ForMember(target => target.Collection,
                            obt =>
                                obt.ConvertUsing(new IdToCollectionConverter(context), src => src.CollectionId));
                    cfg.CreateMap<CornerUpdateDto, Corner>()
                        .ForMember(target => target.Collection,
                            obt =>
                                obt.ConvertUsing(new IdToCollectionConverter(context), src => src.CollectionId));
                    cfg.CreateMap<SofaUpdateDto, Sofa>()
                        .ForMember(target => target.Collection,
                            obt => 
                                obt.ConvertUsing(new IdToCollectionConverter(context), src => src.CollectionId));
                    cfg.CreateMap<TableUpdateDto, Table>()
                        .ForMember(target => target.Collection,
                            obt => 
                                obt.ConvertUsing(new IdToCollectionConverter(context), src => src.CollectionId));
                    cfg.CreateMap<CustomerUpdateDto, Customer>()
                        .ForMember(target => target.Orders,
                            obt =>
                                obt.ConvertUsing(new EnumerableIdToEnumerableOrderConverter(context),
                                    src => src.OrderIds))
                        .ForMember(target => target.Address,
                            obt =>
                                obt.ConvertUsing(new IdToAddressConverter(context), src => src.AddressId));
                    cfg.CreateMap<EmployeeUpdateDto, Employee>()
                        .ForMember(target => target.JobTitles,
                            obt => 
                                obt.ConvertUsing(new StringToJobTitlesConverter()))
                        .ForMember(target => target.Permission,
                            obt =>
                                obt.ConvertUsing(new StringToPermissionConverter()))
                        .ForMember(target => target.Address,
                            obt =>
                                obt.ConvertUsing(new IdToAddressConverter(context),src =>
                                    src.AddressId));
                }
            );

            return config.CreateMapper();
        }
    }
}