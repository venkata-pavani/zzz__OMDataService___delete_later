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

        public DbSet<ContractStatusType> ContractStatusTypes { get; set; }

        public DbSet<ContractPricingStatusType> ContractPricingStatusTypes { get; set; }

        public DbSet<OfferStatusType> OfferStatusTypes { get; set; }

        public DbSet<ContractExportStatusType> ContractExportStatusTypes { get; set; }

        public DbSet<BidsheetsHistory> BidsheetsHistory { get; set; }

        public DbSet<GridLayout> GridLayouts { get; set; }

        public DbSet<Note> Notes { get; set; }

        public DbSet<NotesActivityType> NotesActivityTypes { get; set; }

        public DbSet<NotesPriorityType> NotesPriorityTypes { get; set; }

        public DbSet<NotesStatusType> NotesStatusTypes { get; set; }

        public DbSet<TickHistoryFutures> TickHistoryFutures { get; set; }
        public DbSet<Emails> Emails { get; set; }
        public DbSet<SystemDefaults> SystemDefaults { get; set; }
         

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(schema: DBGlobals.SchemaName);

            modelBuilder.Entity<ContractSearchResult>().HasNoKey();

            modelBuilder.Entity<ContractGraphData>().HasNoKey();

            modelBuilder.Entity<ContractPricing>().HasNoKey();

            modelBuilder.Entity<ContractAmendment>().HasNoKey();

            modelBuilder.Entity<Customer>().HasNoKey();

            modelBuilder.Entity<NoteSearchResult>().HasNoKey();

            base.OnModelCreating(modelBuilder);
        }
    }
}
