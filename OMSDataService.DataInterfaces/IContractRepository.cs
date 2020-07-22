using OMSDataService.DomainObjects.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMSDataService.DataInterfaces
{
    public interface IContractRepository
    {
        Task<List<OfferSearchResult>> GetOffers(int accountId);
        Task<List<ContractSearchResult>> GetContracts(string accountExternalRef);
        Task<ContractDTO> GetContract(int contractId);
        void AddContract(Contract contract, ContractDetail contractDetail);
        void UpdateContract(Contract contract, ContractDetail contractDetail);
        Task<List<ContractSearchResult>> SearchContracts(string contractTransactionTypeExternalRef, string locationExternalRef, string commodityExternalRef, string customerName,
                                                         DateTime? contractDate, DateTime? deliveryBeginDate, DateTime? deliveryEndDate);
        Task<List<OfferSearchResult>> SearchOffers(int? contractTransactionTypeID, int? locationID, int? commodityID, string customerName,
                                                   DateTime? offerDate, DateTime? deliveryBeginDate, DateTime? deliveryEndDate);
        Task<List<ContractPricing>> GetContractPricings(int contractNumber);
        Task<List<ContractAmendment>> GetContractAmendments(int contractNumber);
    }
}
