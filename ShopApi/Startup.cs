using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopApi.DAL;
using ShopApi.Extensions;
using ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Chair;
using ShopApi.Models.Furnitures.FurnitureImplmentation;
using ShopApi.Profiles.Converters;
using ShopApi.Profiles.Converters.IdToCollection;

namespace ShopApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ShopDbContext>(opt => 
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                );

            services.AddAutoMapper(typeof(Startup));

            services.AddFurnitureConfig().AddOrderConfig().AddPeopleConfig()
                .AddCollectionConfig().AddCollectionConfig().AddAddressesConfig()
                .AddConvertersConfig();

            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}");
            });
        }
    }
}