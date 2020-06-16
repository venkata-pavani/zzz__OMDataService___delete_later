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

        public async Task<List<AccountType>> GetAccountTypes()
        {
            var list = await _context.AccountTypes
                .OrderBy(x => x.AccountTypeDescription)
                    .ToListAsync();

            return list;
        }



    }


}
