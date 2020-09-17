﻿using System.Collections.Generic;
using AutoMapper;
using ShopApi.DAL;
using ShopApi.Models.Dtos.Collection;
using ShopApi.Models.Dtos.Furniture;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Chair;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Corner;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Sofa;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Table;
using ShopApi.Models.Dtos.Orders;
using ShopApi.Models.Dtos.People;
using ShopApi.Models.Dtos.People.Address;
using ShopApi.Models.Dtos.People.Customer;
using ShopApi.Models.Dtos.People.Employee;
using ShopApi.Models.Furnitures;
using ShopApi.Models.Furnitures.FurnitureImplmentation;
using ShopApi.Models.Orders;
using ShopApi.Models.People;
using ShopApi.Profiles.Converters;
using ShopApi.Profiles.Converters.EnumerbleIdToEnumerableOrder;
using ShopApi.Profiles.Converters.IdToAddress;
using ShopApi.Profiles.Converters.IdToCollection;
using ShopApi.Profiles.Converters.JobTitlesToString;
using ShopApi.Profiles.Converters.PermissionToString;
using ShopApi.Profiles.Converters.StringToJobTitles;
using ShopApi.Profiles.Converters.StringToPermission;
using ShopApi.Profiles.Converters.StringToStatus;

namespace ShopApi.Profiles
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            MapRead();
            
            MapFromCreate();
        }

        private void MapRead()
        {
            //Model => ReadDto
            CreateMap<Chair, ChairReadDto>();
            CreateMap<Collection, CollectionReadDto>();
            CreateMap<Corner, CornerReadDto>();
            CreateMap<Sofa, SofaReadDto>();
            CreateMap<Table, TableReadDto>();
            CreateMap<Furniture, FurnitureReadDto>();
            CreateMap<FurnitureCount, FurnitureCountReadDto>();

            CreateMap<Collection, CollectionReadDto>();
            
            CreateMap<Order, OrderReadDto>()
                .ForMember(target => target.Furnitures,
                    obj => obj.MapFrom(src => src.Furnitures));
            CreateMap<Address, AddressReadDto>();
            CreateMap<Customer, CustomerReadDto>();
            CreateMap<Employee, EmployeeReadDto>()
                .ForMember(target => target.JobTitles,
                    obt =>
                        obt.ConvertUsing<IJobTitlesToStringConverter, JobTitles>())
                .ForMember(target => target.Permission,
                    obt =>
                        obt.ConvertUsing<IPermissionToStringConverter, Permission>());
        }

        private void MapFromCreate()
        {
            //CreateDto => Model
            CreateMap<CollectionCreateDto, Collection>();
            
            MapSpecialFurnitureCreate();
            
            CreateMap<FurnitureCountCreateDto, FurnitureCount>();
            CreateMap<OrderCreateDto, Order>()
                .ForMember(target => target.Status,
                    obt => obt.ConvertUsing<IStringToStatusConverter, string>());
            CreateMap<AddressCreateDto, Address>();
            CreateMap<CustomerCreateDto, Customer>()
                .ForMember(target => target.Orders,
                    obt =>
                        obt.ConvertUsing<IEnumerableIdToEnumerableOrderConverter, IEnumerable<int>>(src =>
                            src.OrderIds))
                .ForMember(target => target.Address,
                    obt =>
                        obt.ConvertUsing<IIdToAddressConverter, int>(src =>
                            src.AddressId));
            
            CreateMap<EmployeeCreateDto, Employee>()
                .ForMember(target => target.JobTitles,
                    obt => 
                        obt.ConvertUsing<IStringToJobTitlesConverter, string>())
                .ForMember(target => target.Permission,
                    obt =>
                        obt.ConvertUsing<IStringToPermissionConverter, string>())
                .ForMember(target => target.Address,
                    obt =>
                        obt.ConvertUsing<IIdToAddressConverter, int>(src =>
                            src.AddressId));
        }

        private void MapSpecialFurnitureCreate()
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
    }
}