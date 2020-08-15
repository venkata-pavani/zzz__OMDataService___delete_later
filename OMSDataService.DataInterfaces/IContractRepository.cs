using OMSDataService.DomainObjects.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMSDataService.DataInterfaces
{
    public interface IContractRepository
    {
        Task<List<OfferSearchResult>> GetOffers(int accountId);
        Task<List<OfferSearchResult>> GetOffersOnContract(int contractNumber);
        Task<List<OfferSearchResult>> GetOffersOnBidsheet(int bidsheetID, bool getOffersByAccountOnly, int? accountID);
        Task<List<ContractSearchResult>> GetContracts(string accountExternalRef);
        Task<ContractDTO> GetContract(int contractId);
        Task<int> AddContract(Contract contract, ContractDetail contractDetail);
        void UpdateContract(Contract contract, ContractDetail contractDetail);
        void ConvertOfferToContract(Contract contract, ContractDetail contractDetail);
        void RollOffer(Contract contract, ContractDetail contractDetail, BidsheetSearchResult bidsheet);
        Task<List<ContractSearchResult>> SearchContracts(string contractTransactionTypeExternalRef, string locationExternalRef, string commodityExternalRef, string commoditySymbol, string customerName,
                                                         string marketZoneExternalRef, string contractTypeExternalRef, string contractStatusTypeExternalRef, string advisorExternalRef,
                                                         DateTime? contractStartDate, DateTime? contractEndDate, DateTime? deliveryBeginStartDate, DateTime? deliveryBeginEndDate,
                                                         DateTime? deliveryEndStartDate, DateTime? deliveryEndEndDate, string contractPricingStatusTypeExternalRef);
        Task<List<OfferSearchResult>> SearchOffers(int? contractTransactionTypeID, int? locationID, int? commodityID, string commoditySymbol, string customerName, int? contractTypeID,
                                                   int? offerStatusTypeID, int? marketZoneID, int? advisorID, DateTime? offerStartDate, DateTime? offerEndDate, DateTime? deliveryBeginStartDate,
                                                   DateTime? deliveryBeginEndDate, DateTime? deliveryEndStartDate, DateTime? deliveryEndEndDate);
        Task<List<ContractPricing>> GetContractPricings(int contractNumber);
        Task<List<ContractAmendment>> GetContractAmendments(int contractNumber);
        Task<List<ContractOfferSearchResult>> GetOffersAndContracts(int accountId);
        Task<List<ContractOfferSearchResult>> SearchOffersAndContracts(int? contractTransactionTypeID, int? locationID, int? commodityID, string commoditySymbol, string customerName,
                                                                       int? contractTypeID, int? marketZoneID, int? advisorID, DateTime? createdStartDate, DateTime? createdEndDate,
                                                                       DateTime? deliveryBeginStartDate, DateTime? deliveryBeginEndDate, DateTime? deliveryEndStartDate,
                                                                       DateTime? deliveryEndEndDate);
    }
}
