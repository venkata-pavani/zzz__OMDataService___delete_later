using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using OMSDataService.DataInterfaces;
using OMSDataService.DataRepositories;
using OMSDataService.EF;
using Serilog;
using System;

namespace OMSDataService.IocMapper
{
    public class IocRegistry : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // register dbContext with container
            //  connection and credentials stored in appsettings for now
            //builder.Register(c =>
            //{
            //    var config = c.Resolve<IConfiguration>();

            //    var opt = new DbContextOptionsBuilder<ApiContext>();
            //    opt.UseSqlServer(config.GetConnectionString("Default"));

            //    return new ApiContext(opt.Options);

            //}).AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<TypeRepository>().As<ITypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ContractRepository>().As<IContractRepository>().InstancePerLifetimeScope();
            builder.RegisterType<BidsheetRepository>().As<IBidsheetRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AccountRepository>().As<IAccountRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HelperRepository>().As<IHelperRepository>().InstancePerLifetimeScope();


            builder.Register<ILogger>((c, p) =>
            {
                return new LoggerConfiguration()
                   .WriteTo.File(path: ".\\Logs\\OMSLogInformationFile.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 365, rollOnFileSizeLimit: true)

                // When using ".UseSerilog()" it will use "Log.Logger".
                .CreateLogger();
            }).SingleInstance();

        }



    }
}
