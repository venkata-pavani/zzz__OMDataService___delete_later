using Microsoft.EntityFrameworkCore;
using OMSDataService.DomainObjects.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OMSDataService.EF
{
    public class ApiContext : DbContext
    {        
        public ApiContext(DbContextOptions options)
            : base(options) { }


        public DbSet<Location> Locations { get; set; }

        public DbSet<Commodity> Commodities{ get; set; }

        public DbSet<Bidsheet> Bidsheets { get; set; }

        public DbSet<Advisor> Advisors { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountType> AccountTypes { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<ContractDetail> ContractDetails { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<ContractTransactionType> ContractTransactionTypes { get; set; }

        public DbSet<ContractType> ContractTypes { get; set; }

        public DbSet<Exchange> Exchanges { get; set; }

        public DbSet<MarketZone> MarketZones { get; set; }      

        public DbSet<Month> Months { get; set; }

        public DbSet<OfferPriceType> OfferPriceTypes { get; set; }

        public DbSet<OfferDurationType> OfferDurationTypes { get; set; }

        public DbSet<OfferType> OfferTypes { get; set; }

        public DbSet<UnitOfMeasure> UnitsOfMeasure { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<County> Counties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(schema: DBGlobals.SchemaName);            

            //modelBuilder.Entity<AaisRate>().HasNoKey(); 
                       

            base.OnModelCreating(modelBuilder);
        }

    }
}
