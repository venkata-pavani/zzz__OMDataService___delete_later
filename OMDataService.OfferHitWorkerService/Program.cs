using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OMSDataService.DataInterfaces;
using OMSDataService.DataRepositories;
using OMSDataService.EF;
using OMSDataService.IocMapper;

namespace OMDataService.OfferHitWorkerService
{
    public class Program
    {

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public Program(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

         
        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureAppConfiguration(config => config.AddUserSecrets(Assembly.GetExecutingAssembly()))
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    //services.AddDbContext<ApiContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default"),
                    //                         sqlServerOptions => sqlServerOptions.CommandTimeout(180)));

                    //services.AddScoped<IHelperRepository, HelperRepository>();

                });
                

        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    // Add any Autofac modules or registrations.
        //    // This is called AFTER ConfigureServices so things you
        //    // register here OVERRIDE things registered in ConfigureServices.
        //    //
        //    // You must have the call to AddAutofac in the Program.Main
        //    // method or this won't be called.
        //    builder.RegisterModule(new IocRegistry());
        //}

    }
}
