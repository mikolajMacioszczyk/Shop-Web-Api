using AutoMapper;
using ShopApi.Models.Dtos.Orders.FurnitureCountDtos;
using ShopApi.Models.Dtos.Orders.OrderDtos;
using ShopApi.Models.Orders;
using ShopApi.Profiles.Converters.StringToStatus;

namespace ShopApi.Tests.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<FurnitureCount, FurnitureCountReadDto>();
            CreateMap<Order, OrderReadDto>()
                .ForMember(target => target.Furnitures,
                    obj => obj.MapFrom(src => src.Furnitures));
            CreateMap<OrderCreateDto, Order>()
                .ForMember(target => target.Status,
                    obt => 
                        obt.ConvertUsing(new StringToStatusConverter()));
            CreateMap<FurnitureCountUpdateDto, FurnitureCount>();
            CreateMap<OrderUpdateDto, Order>()
                .ForMember(target => target.Status,
                    obt => obt.ConvertUsing(new StringToStatusConverter()));
        }   
    }
}