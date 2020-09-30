using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OMSDataService.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace OMDataService.OfferHitWorkerService.Classes
{
    public static class Global
    {
        public static DbContextOptions GetConnectionString()
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();

            var builder = new DbContextOptionsBuilder<ApiContext>();
            builder.UseSqlServer(configuration.GetConnectionString("Default"),
                                             sqlServerOptions => sqlServerOptions.CommandTimeout(180));


            //builder.UseSqlServer(
            //  "Server=tcp:tma-sqlserver.database.windows.net;Initial Catalog=MobileDataService;Persist Security Info=False;User ID=paige;Password=newyork1234$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            ////"Server=SQL2014VM;Initial Catalog=TMADB;Persist Security Info=False;User ID=TMAUser;Password=TMA1646;");
            //builder.UseSqlServer(
            //    "Server=SQL2014VM;Database=TMA;Trusted_Connection=True;MultipleActiveResultSets=true");

            //@"Server=172.27.227.82;initial catalog=NRS_OM;Integrated Security=false;user Id=CUUser;password=4notrs2getN;MultipleActiveResultSets=True;App=EntityFramework;");
            return builder.Options;
        }
    }
}
