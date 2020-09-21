using AutoMapper;
using ShopApi.Models.Dtos.Orders.FurnitureCountDtos;
using ShopApi.Models.Dtos.Orders.OrderDtos;
using ShopApi.Models.Orders;
using ShopApi.Profiles.Converters.StringToStatus;

namespace ShopApi.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            MapModelToRead();
            MapUpdateToModel();
            MapCreateToModel();   
        }

        private void MapModelToRead()
        {
            CreateMap<FurnitureCount, FurnitureCountReadDto>();
            CreateMap<Order, OrderReadDto>()
                .ForMember(target => target.Furnitures,
                    obj => obj.MapFrom(src => src.Furnitures));

        }

        private void MapCreateToModel()
        {
            CreateMap<FurnitureCountCreateDto, FurnitureCount>();
            CreateMap<OrderCreateDto, Order>()
                .ForMember(target => target.Status,
                    obt => obt.ConvertUsing<IStringToStatusConverter, string>());

        }

        private void MapUpdateToModel()
        {
            CreateMap<FurnitureCountUpdateDto, FurnitureCount>();
            CreateMap<OrderUpdateDto, Order>()
                .ForMember(target => target.Status,
                    obt => obt.ConvertUsing<IStringToStatusConverter, string>())
                .ForMember(target => target.Furnitures,
                    obt =>
                        obt.MapFrom(src => src.Furnitures));
        }
    }
}