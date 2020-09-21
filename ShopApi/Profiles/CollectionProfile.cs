using AutoMapper;
using Microsoft.VisualBasic;
using ShopApi.Models.Dtos.Collection;

namespace ShopApi.Profiles
{
    public class CollectionProfile : Profile
    {
        public CollectionProfile()
        {
            CreateMap<Collection, CollectionReadDto>();
            CreateMap<CollectionUpdateDto, Collection>();
            CreateMap<CollectionCreateDto, Collection>();
        }
    }
}