using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopApi.DAL;

namespace ShopApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host =  CreateHostBuilder(args).Build(); // .Run();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ShopDbContext>();
                    await DataSeeder.InitializeAsync(context);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error {e} occured while seeding the database");
                }
            }
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}