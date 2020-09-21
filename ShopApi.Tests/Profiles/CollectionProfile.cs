using AutoMapper;
using ShopApi.Models.Dtos.Collection;
using ShopApi.Models.Furnitures;

namespace ShopApi.Tests.Profiles
{
    public class CollectionProfile : Profile
    {
        public CollectionProfile()
        {
            CreateMap<Collection, CollectionReadDto>();
            CreateMap<CollectionCreateDto, Collection>();
            CreateMap<CollectionUpdateDto, Collection>();
        }   
    }
}