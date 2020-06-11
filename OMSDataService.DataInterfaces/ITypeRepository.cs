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

        Task<List<ContractType>> GetContractTypes();

        Task<List<Customer>> GetCustomers();

        Task<List<Month>> GetMonths();

        Task<List<OfferPriceType>> GetOfferPriceTypes();

        Task<List<OfferType>> GetOfferTypes();

        Task<List<UnitOfMeasure>> GetUnitsOfMeasure();
    }
}
