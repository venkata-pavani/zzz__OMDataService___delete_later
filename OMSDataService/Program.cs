using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace OMSDataService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services.AddAutofac())
                .UseStartup<Startup>()
                .Build();


        //    public static void Main(string[] args)
        //    {
        //        CreateHostBuilder(args).Build().Run();
        //    }

        //    public static IHostBuilder CreateHostBuilder(string[] args) =>
        //        Host.CreateDefaultBuilder(args)
        //            .ConfigureWebHostDefaults(webBuilder =>
        //            {
        //                webBuilder.UseStartup<Startup>();
        //            });
        //}
    }
}
