using System.Collections.Generic;
using AutoMapper;
using ShopApi.Models.Dtos.People.Base;
using ShopApi.Models.Dtos.People.Customer;
using ShopApi.Models.Dtos.People.Employee;
using ShopApi.Models.People;
using ShopApi.Profiles.Converters.EnumerbleIdToEnumerableOrder;
using ShopApi.Profiles.Converters.IdToAddress;
using ShopApi.Profiles.Converters.JobTitlesToString;
using ShopApi.Profiles.Converters.PermissionToString;
using ShopApi.Profiles.Converters.StringToJobTitles;
using ShopApi.Profiles.Converters.StringToPermission;

namespace ShopApi.Profiles
{
    public class PeopleProfile : Profile
    {
        public PeopleProfile()
        {
            MapModelToRead();
            MapCreateToModel();
            MapUpdateToModel();
        }

        private void MapModelToRead()
        {
            CreateMap<Person, PersonReadDto>();
            CreateMap<Customer, CustomerReadDto>();
            CreateMap<Employee, EmployeeReadDto>()
                .ForMember(target => target.JobTitles,
                    obt =>
                        obt.ConvertUsing<IJobTitlesToStringConverter, JobTitles>())
                .ForMember(target => target.Permission,
                    obt =>
                        obt.ConvertUsing<IPermissionToStringConverter, Permission>());
        }

        private void MapCreateToModel()
        {
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

        private void MapUpdateToModel()
        {
            CreateMap<CustomerUpdateDto, Customer>()
                .ForMember(target => target.Orders,
                    obt =>
                        obt.ConvertUsing<IEnumerableIdToEnumerableOrderConverter, IEnumerable<int>>(src =>
                            src.OrderIds))
                .ForMember(target => target.Address,
                    obt =>
                        obt.ConvertUsing<IIdToAddressConverter, int>(src =>
                            src.AddressId));
            
            CreateMap<EmployeeUpdateDto, Employee>()
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
    }
}