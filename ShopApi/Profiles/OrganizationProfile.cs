using AutoMapper;
using ShopApi.Models.Dtos.Furniture;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations;
using ShopApi.Models.Dtos.Orders;
using ShopApi.Models.Dtos.People;
using ShopApi.Models.Furnitures;
using ShopApi.Models.Furnitures.FurnitureImplmentation;
using ShopApi.Models.Orders;
using ShopApi.Models.People;

namespace ShopApi.Profiles
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Chair, ChairReadDto>();
            CreateMap<Collection, CollectionReadDto>();
            CreateMap<Corner, CornerReadDto>();
            CreateMap<Sofa, SofaReadDto>();
            CreateMap<Table, TableReadDto>();
            CreateMap<Furniture, FurnitureReadDto>();
            CreateMap<FurnitureCount, FurnitureCountReadDto>();
            
            CreateMap<Order, OrderReadDto>()
                .ForMember(target => target.Furnitures,
                    obj => obj.MapFrom(src => src.Furnitures));
            CreateMap<Address, AddressReadDto>();
            CreateMap<Customer, CustomerReadDto>();
            CreateMap<Employee, EmployeeReadDto>();
        }
    }
}