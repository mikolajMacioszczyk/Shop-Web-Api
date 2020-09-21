using AutoMapper;
using ShopApi.DAL;

namespace ShopApi.Tests
{
    public static class MapperInitializer
    {
        public static IMapper GetMapper(ShopDbContext context)
        {
            var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new Profiles.PeopleProfile(context));
                    cfg.AddProfile(new Profiles.FurnitureProfile(context));
                    cfg.AddProfile(new Profiles.CollectionProfile());
                    cfg.AddProfile(new Profiles.AddressProfile());
                    cfg.AddProfile(new Profiles.OrderProfile());
                }
            );
            return config.CreateMapper();
        }
    }
}