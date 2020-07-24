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
                                                         string marketZoneExternalRef, string contractTypeExternalRef, string contractStatusTypeExternalRef, string advisorExternalRef,
                                                         DateTime? contractStartDate, DateTime? contractEndDate, DateTime? deliveryBeginStartDate, DateTime? deliveryBeginEndDate,
                                                         DateTime? deliveryEndStartDate, DateTime? deliveryEndEndDate);
        Task<List<OfferSearchResult>> SearchOffers(int? contractTransactionTypeID, int? locationID, int? commodityID, string customerName, int? contractTypeID, int? offerStatusTypeID,
                                                   int? marketZoneID, int? advisorID, DateTime? offerStartDate, DateTime? offerEndDate, DateTime? deliveryBeginStartDate, DateTime? deliveryBeginEndDate,
                                                   DateTime? deliveryEndStartDate, DateTime? deliveryEndEndDate);
        Task<List<ContractPricing>> GetContractPricings(int contractNumber);
        Task<List<ContractAmendment>> GetContractAmendments(int contractNumber);
    }
}
