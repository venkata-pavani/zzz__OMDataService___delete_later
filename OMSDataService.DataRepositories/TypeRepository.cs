using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OMSDataService.DataInterfaces;
using OMSDataService.DomainObjects.Models;
using OMSDataService.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMSDataService.DataRepositories
{
    public class TypeRepository : ITypeRepository
    {
        private readonly IMapper _mapper;
        private ApiContext _context;

        public TypeRepository(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Location>> GetLocations()
        {
            var list = await _context.Locations
                .OrderBy(x => x.LocationName)
                    .ToListAsync();          

            return list;
        }

        public async Task<List<Commodity>> GetCommodities()
        {
            var list = await _context.Commodities
                .OrderBy(x => x.CommodityName)
                    .ToListAsync();

            return list;
        }

        public async Task<List<ContractTransactionType>> GetContractTransactionTypes()
        {
            var list = await _context.ContractTransactionTypes
                .OrderBy(x => x.Description)
                    .ToListAsync();

            return list;
        }

        public async Task<List<ContractStatusType>> GetContractStatusTypes()
        {
            return await _context.ContractStatusTypes.OrderBy(x => x.Description).ToListAsync();
        }

        public async Task<List<ContractType>> GetContractTypes()
        {
            var list = await _context.ContractTypes
                .OrderBy(x => x.Description)
                    .ToListAsync();

            return list;
        }       

        public async Task<List<Month>> GetMonths()
        {
            var list = await _context.Months
                .OrderBy(x => x.MonthID)
                    .ToListAsync();

            return list;
        }

        public async Task<List<OfferDurationType>> GetOfferDurationTypes()
        {
            var list = await _context.OfferDurationTypes
                .OrderBy(x => x.Description)
                    .ToListAsync();

            return list;
        }

        public async Task<List<OfferPriceType>> GetOfferPriceTypes()
        {
            var list = await _context.OfferPriceTypes
                .OrderBy(x => x.OfferPriceTypeDescription)
                    .ToListAsync();

            return list;
        }


        public async Task<List<OfferType>> GetOfferTypes()
        {
            var list = await _context.OfferTypes
                .OrderBy(x => x.OfferTypeDescription)
                    .ToListAsync();

            return list;
        }

        public async Task<List<UnitOfMeasure>> GetUnitsOfMeasure()
        {
            var list = await _context.UnitsOfMeasure
                .OrderBy(x => x.UOM)
                    .ToListAsync();

            return list;
        }

        public async Task<List<Account>> GetAccounts()
        {
            var list = await _context.Accounts
                .OrderBy(x => x.AccountName)
                    .ToListAsync();

            return list;
        }

        public async Task<List<AccountSearchResult>> SearchAccounts(string accountName, string externalRef)
        {
            return await (from a in _context.Accounts
                          join s in _context.States on a.StateID equals s.StateID
                          where (string.IsNullOrEmpty(accountName) || a.AccountName.Contains(accountName)) &&
                                (string.IsNullOrEmpty(externalRef) || a.ExternalRef.Contains(externalRef))
                          select new AccountSearchResult
                          {
                              AccountID = a.AccountID,
                              AccountName = a.AccountName,
                              Address1 = a.Address1,
                              Address2 = a.Address2,
                              City = a.City,
                              ExternalRef = a.ExternalRef,
                              ExternalRefName = a.ExternalRefName,
                              Fax = a.Fax,
                              Phone1 = a.Phone1,
                              Phone2 = a.Phone2,
                              State = s.StateName,
                              WebAddress = a.WebAddress,
                              Zip = a.Zip
                          }).OrderBy(a => a.AccountName).ToListAsync();
        }

        public async Task<List<AccountType>> GetAccountTypes()
        {
            return await _context.AccountTypes.OrderBy(x => x.AccountTypeDescription).ToListAsync();
        }

        public async Task<List<Advisor>> GetAdvisors()
        {
            return await _context.Advisors.OrderBy(a => a.AdvisorName).ToListAsync();
        }

        public async Task<List<MarketZone>> GetMarketZones()
        {
            return await _context.MarketZones.OrderBy(m => m.MarketZoneName).ToListAsync();
        }

        public async Task<List<ContractPricingStatusType>> GetContractPricingStatusTypes()
        {
            return await _context.ContractPricingStatusTypes.OrderBy(c => c.Description).ToListAsync();
        }

        public async Task<List<OfferStatusType>> GetOfferStatusTypes()
        {
            return await _context.OfferStatusTypes.OrderBy(o => o.OfferStatusTypeDescription).ToListAsync();
        }
    }
}
