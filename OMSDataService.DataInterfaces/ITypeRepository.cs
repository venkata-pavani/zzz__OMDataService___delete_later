using OMSDataService.DomainObjects.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMSDataService.DataInterfaces
{
    public interface ITypeRepository
    {
        Task<List<Location>> GetLocations(bool sortForDropDownList);

        Task<List<Commodity>> GetCommodities(bool sortForDropDownList);

        Task<List<ContractTransactionType>> GetContractTransactionTypes(bool sortForDropDownList);

        Task<List<ContractStatusType>> GetContractStatusTypes(bool sortForDropDownList);

        Task<List<ContractType>> GetContractTypes(bool sortForDropDownList);
        
        Task<List<Month>> GetMonths(bool sortForDropDownList);

        Task<List<OfferDurationType>> GetOfferDurationTypes(bool sortForDropDownList);

        Task<List<OfferPriceType>> GetOfferPriceTypes(bool sortForDropDownList);

        Task<List<OfferType>> GetOfferTypes(bool sortForDropDownList);

        Task<List<UnitOfMeasure>> GetUnitsOfMeasure(bool sortForDropDownList);

        Task<List<Account>> GetAccounts(bool sortForDropDownList);

        Task<List<AccountSearchResult>> SearchAccounts(string accountName, string externalRef);

        Task<List<AccountType>> GetAccountTypes(bool sortForDropDownList);

        Task<List<Advisor>> GetAdvisors(bool sortForDropDownList);

        Task<List<MarketZone>> GetMarketZones(bool sortForDropDownList);

        Task<List<ContractPricingStatusType>> GetContractPricingStatusTypes(bool sortForDropDownList);

        Task<List<OfferStatusType>> GetOfferStatusTypes(bool sortForDropDownList);

        Task<List<ContractExportStatusType>> GetContractExportStatusTypes(bool sortForDropDownList);
    }
}
