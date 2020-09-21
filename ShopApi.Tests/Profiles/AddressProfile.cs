using AutoMapper;
using ShopApi.Models.Dtos.Address;
using ShopApi.Models.People;

namespace ShopApi.Tests.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressReadDto>();
            CreateMap<AddressCreateDto, Address>();
            CreateMap<AddressUpdateDto, Address>();
        }
    }
}