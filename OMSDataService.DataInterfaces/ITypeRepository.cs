using OMSDataService.DomainObjects.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMSDataService.DataInterfaces
{
    public interface ITypeRepository
    {
        Task<List<Location>> GetLocations();

        Task<List<Commodity>> GetCommodities();

        Task<List<ContractTransactionType>> GetContractTransactionTypes();

        Task<List<ContractStatusType>> GetContractStatusTypes();

        Task<List<ContractType>> GetContractTypes();
        
        Task<List<Month>> GetMonths();

        Task<List<OfferDurationType>> GetOfferDurationTypes();

        Task<List<OfferPriceType>> GetOfferPriceTypes();

        Task<List<OfferType>> GetOfferTypes();

        Task<List<UnitOfMeasure>> GetUnitsOfMeasure();

        Task<List<Account>> GetAccounts();

        Task<List<AccountSearchResult>> SearchAccounts(string accountName, string externalRef);

        Task<List<AccountType>> GetAccountTypes();

        Task<List<Advisor>> GetAdvisors();

        Task<List<MarketZone>> GetMarketZones();

        Task<List<ContractPricingStatusType>> GetContractPricingStatusTypes();

        Task<List<OfferStatusType>> GetOfferStatusTypes();
    }
}
