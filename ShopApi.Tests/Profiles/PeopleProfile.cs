using AutoMapper;
using ShopApi.DAL;
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

namespace ShopApi.Tests.Profiles
{
    public class PeopleProfile : Profile
    {
        private readonly ShopDbContext _context;
        public PeopleProfile(ShopDbContext context)
        {
            _context = context;
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
                        obt.ConvertUsing(new JobTitlesToStringConverter()))
                .ForMember(target => target.Permission,
                    obt =>
                        obt.ConvertUsing(new PermissionToStringConverter()));
        }

        private void MapCreateToModel()
        {
            CreateMap<CustomerCreateDto, Customer>()
                .ForMember(target => target.Orders,
                    obt =>
                        obt.ConvertUsing(new EnumerableIdToEnumerableOrderConverter(_context),
                            src => src.OrderIds))
                .ForMember(target => target.Address,
                    obt =>
                        obt.ConvertUsing(new IdToAddressConverter(_context), src => src.AddressId));
            CreateMap<EmployeeCreateDto, Employee>()
                .ForMember(target => target.JobTitles,
                    obt =>
                        obt.ConvertUsing(new StringToJobTitlesConverter()))
                .ForMember(target => target.Permission,
                    obt =>
                        obt.ConvertUsing(new StringToPermissionConverter()))
                .ForMember(target => target.Address,
                    obt =>
                        obt.ConvertUsing(new IdToAddressConverter(_context), src => src.AddressId));

        }

        private void MapUpdateToModel()
        {
            CreateMap<CustomerUpdateDto, Customer>()
                .ForMember(target => target.Orders,
                    obt =>
                        obt.ConvertUsing(new EnumerableIdToEnumerableOrderConverter(_context),
                            src => src.OrderIds))
                .ForMember(target => target.Address,
                    obt =>
                        obt.ConvertUsing(new IdToAddressConverter(_context), src => src.AddressId));
            CreateMap<EmployeeUpdateDto, Employee>()
                .ForMember(target => target.JobTitles,
                    obt => 
                        obt.ConvertUsing(new StringToJobTitlesConverter()))
                .ForMember(target => target.Permission,
                    obt =>
                        obt.ConvertUsing(new StringToPermissionConverter()))
                .ForMember(target => target.Address,
                    obt =>
                        obt.ConvertUsing(new IdToAddressConverter(_context),src =>
                            src.AddressId));
        }
    }
}