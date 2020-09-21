using AutoMapper;
using ShopApi.Models.Dtos.Furniture.Base;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Chair;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Corner;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Sofa;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Table;
using ShopApi.Models.Furnitures;
using ShopApi.Models.Furnitures.FurnitureImplmentation;
using ShopApi.Profiles.Converters.IdToCollection;

namespace ShopApi.Profiles
{
    public class FurnitureProfile : Profile
    {
        public FurnitureProfile()
        {
            MapModelToRead();   
            MapCreateToModel();
            MapUpdateToModel();
        }

        private void MapModelToRead()
        {
            CreateMap<Chair, ChairReadDto>();
            CreateMap<Corner, CornerReadDto>();
            CreateMap<Sofa, SofaReadDto>();
            CreateMap<Table, TableReadDto>();
            CreateMap<Furniture, FurnitureReadDto>();
        }

        private void MapCreateToModel()
        {
            CreateMap<ChairCreateDto, Chair>()
                .ForMember(target => target.Collection,
                    obt => 
                        obt.ConvertUsing<IIdToCollectionConverter, int>(src => src.CollectionId));
            CreateMap<CornerCreateDto, Corner>()
                .ForMember(target => target.Collection,
                    obt => 
                        obt.ConvertUsing<IIdToCollectionConverter, int>(src => src.CollectionId));
            CreateMap<SofaCreateDto, Sofa>()
                .ForMember(target => target.Collection,
                    obt => 
                        obt.ConvertUsing<IIdToCollectionConverter, int>(src => src.CollectionId));
            CreateMap<TableCreateDto, Table>()
                .ForMember(target => target.Collection,
                    obt => 
                        obt.ConvertUsing<IIdToCollectionConverter, int>(src => src.CollectionId));

        }

        private void MapUpdateToModel()
        {
            CreateMap<CharUpdateDto, Chair>()
                .ForMember(target => target.Collection,
                    obt => 
                        obt.ConvertUsing<IIdToCollectionConverter, int>(src => src.CollectionId));
            CreateMap<CornerUpdateDto, Corner>()
                .ForMember(target => target.Collection,
                    obt => 
                        obt.ConvertUsing<IIdToCollectionConverter, int>(src => src.CollectionId));
            CreateMap<SofaUpdateDto, Sofa>()
                .ForMember(target => target.Collection,
                    obt => 
                        obt.ConvertUsing<IIdToCollectionConverter, int>(src => src.CollectionId));
            CreateMap<TableUpdateDto, Table>()
                .ForMember(target => target.Collection,
                    obt => 
                        obt.ConvertUsing<IIdToCollectionConverter, int>(src => src.CollectionId));
        }
    }
}