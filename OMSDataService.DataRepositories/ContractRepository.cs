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
    public class ContractRepository : IContractRepository
    {

        private readonly IMapper _mapper;
        private ApiContext _context;

        public ContractRepository(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OfferSearchResult>> GetOffers(int accountId)
        {
            return await (from c in _context.Contracts
                          join cd in _context.ContractDetails on c.ContractID equals cd.ContractID
                          join a in _context.Accounts on cd.AccountID equals a.AccountID
                          join cmd in _context.Commodities on cd.CommodityID equals cmd.CommodityID
                          join ct in _context.ContractTypes on cd.ContractTypeID equals ct.ContractTypeID
                          join l in _context.Locations on cd.LocationID equals l.LocationID
                          join ctt in _context.ContractTransactionTypes on c.ContractTransactionTypeID equals ctt.ContractTransactionTypeID
                          join ost in _context.OfferStatusTypes on cd.OfferStatusTypeID equals ost.OfferStatusTypeID
                          join m in _context.MarketZones on cd.MarketZoneID equals m.MarketZoneID
                          join ad in _context.Advisors on cd.AdvisorID equals ad.AdvisorID
                          where cd.AccountID == accountId && cd.Offer.Value == true
                          orderby c.ContractID
                          select new OfferSearchResult
                          {
                              AccountID = a.ExternalRef,
                              AccountName = a.AccountName,
                              Basis = cd.OfferBasis,
                              CashPrice = cd.OfferCashPrice,
                              CommodityID = cd.CommodityID,
                              CommodityName = cmd.CommodityName,
                              ContractDate = cd.ContractDetailOfferDate.HasValue ? cd.ContractDetailOfferDate.Value.ToString("MM/dd/yyyy") : "",
                              ContractDateTime = cd.ContractDetailOfferDate,
                              ContractID = c.ContractID,
                              ContractNumber = c.ContractNumber,
                              ContractTypeID = cd.ContractTypeID,
                              ContractTypeName = ct.ContractTypeCode,
                              DeliveryEndDate = cd.DeliveryEndDate.HasValue ? cd.DeliveryEndDate.Value.ToString("MM/dd/yyyy") : "",
                              DeliveryEnd = cd.DeliveryEndDate,
                              DeliveryStartDate = cd.DeliveryStartDate.HasValue ? cd.DeliveryStartDate.Value.ToString("MM/dd/yyyy") : "",
                              DeliveryStart = cd.DeliveryStartDate,
                              FuturesPrice = cd.OfferFutures,
                              LocationID = cd.LocationID.HasValue ? cd.LocationID.Value : 0,
                              LocationName = l.LocationName,
                              Quantity = cd.Quantity,
                              ContractTransactionType = ctt.Description,
                              OfferStatusType = ost.OfferStatusTypeDescription,
                              MarketZone = m.Description,
                              AdvisorName = ad.AdvisorName
                          }).Take(1000).ToListAsync();
        }

        public async Task<List<OfferSearchResult>> GetOffersOnContract(int contractNumber)
        {
            return await (from c in _context.Contracts
                          join cd in _context.ContractDetails on c.ContractID equals cd.ContractID
                          join a in _context.Accounts on cd.AccountID equals a.AccountID
                          join cmd in _context.Commodities on cd.CommodityID equals cmd.CommodityID
                          join ct in _context.ContractTypes on cd.ContractTypeID equals ct.ContractTypeID
                          join l in _context.Locations on cd.LocationID equals l.LocationID
                          join ctt in _context.ContractTransactionTypes on c.ContractTransactionTypeID equals ctt.ContractTransactionTypeID
                          join ost in _context.OfferStatusTypes on cd.OfferStatusTypeID equals ost.OfferStatusTypeID
                          join m in _context.MarketZones on cd.MarketZoneID equals m.MarketZoneID
                          join ad in _context.Advisors on cd.AdvisorID equals ad.AdvisorID
                          where c.ContractNumber == contractNumber.ToString() && cd.Offer.Value == true
                          orderby c.ContractID
                          select new OfferSearchResult
                          {
                              AccountID = a.ExternalRef,
                              AccountName = a.AccountName,
                              Basis = cd.OfferBasis,
                              CashPrice = cd.OfferCashPrice,
                              CommodityID = cd.CommodityID,
                              CommodityName = cmd.CommodityName,
                              ContractDate = cd.ContractDetailOfferDate.HasValue ? cd.ContractDetailOfferDate.Value.ToString("MM/dd/yyyy") : "",
                              ContractDateTime = cd.ContractDetailOfferDate,
                              ContractID = c.ContractID,
                              ContractNumber = c.ContractNumber,
                              ContractTypeID = cd.ContractTypeID,
                              ContractTypeName = ct.ContractTypeCode,
                              DeliveryEndDate = cd.DeliveryEndDate.HasValue ? cd.DeliveryEndDate.Value.ToString("MM/dd/yyyy") : "",
                              DeliveryEnd = cd.DeliveryEndDate,
                              DeliveryStartDate = cd.DeliveryStartDate.HasValue ? cd.DeliveryStartDate.Value.ToString("MM/dd/yyyy") : "",
                              DeliveryStart = cd.DeliveryStartDate,
                              FuturesPrice = cd.OfferFutures,
                              LocationID = cd.LocationID.HasValue ? cd.LocationID.Value : 0,
                              LocationName = l.LocationName,
                              Quantity = cd.Quantity,
                              ContractTransactionType = ctt.Description,
                              OfferStatusType = ost.OfferStatusTypeDescription,
                              MarketZone = m.Description,
                              AdvisorName = ad.AdvisorName
                          }).Take(1000).ToListAsync();
        }

        [Obsolete]
        public async Task<List<ContractSearchResult>> GetContracts(string accountExternalRef)
        {
            return await _context.Query<ContractSearchResult>().FromSqlRaw("Execute dbo.GetContracts @AccountExternalRef = {0}", accountExternalRef).ToListAsync();
        }

        public async Task<ContractDTO> GetContract(int contractId)
        {
            var dto = new ContractDTO();
            dto.Contract = null;
            dto.ContractDetail = null;

            var contract = await _context.Contracts.FirstOrDefaultAsync(c => c.ContractID == contractId);
            var contractDetail = await _context.ContractDetails.FirstOrDefaultAsync(cd => cd.ContractID == contractId);

            if (contract != null && contractDetail != null)
            {
                dto.Contract = contract;
                dto.ContractDetail = contractDetail;
            }

            return dto;
        }

        public void AddContract(Contract contract, ContractDetail contractDetail)
        {
            contract.AddDate = contractDetail.AddDate = contract.ChgDate = contractDetail.ChgDate = DateTime.Now;

            _context.Contracts.Add(contract);
            _context.SaveChanges();

            contractDetail.ContractID = contract.ContractID;

            _context.ContractDetails.Add(contractDetail);
            _context.SaveChanges();
        }

        public void UpdateContract(Contract contract, ContractDetail contractDetail)
        {
            contract.ChgDate = contractDetail.ChgDate = DateTime.Now;

            _context.Entry(contract).State = EntityState.Modified;
            _context.Entry(contractDetail).State = EntityState.Modified;
            _context.SaveChanges();
        }

        [Obsolete]
        public async Task<List<ContractSearchResult>> SearchContracts(string contractTransactionTypeExternalRef, string locationExternalRef, string commodityExternalRef,
                                                                      string customerName, string marketZoneExternalRef, string contractTypeExternalRef, string contractStatusTypeExternalRef,
                                                                      string advisorExternalRef, DateTime? contractStartDate, DateTime? contractEndDate, DateTime? deliveryBeginStartDate,
                                                                      DateTime? deliveryBeginEndDate, DateTime? deliveryEndStartDate, DateTime? deliveryEndEndDate)
        {
            return await _context.Query<ContractSearchResult>().FromSqlRaw("Execute dbo.SearchContracts " +
                                                                           "@ContractTransactionTypeExternalRef = {0}," +
                                                                           "@LocationExternalRef = {1}, " +
                                                                           "@CommodityExternalRef = {2}, " +
                                                                           "@CustomerName = {3}, " +
                                                                           "@MarketZoneExternalRef = {4}, " +
                                                                           "@ContractTypeExternalRef = {5}, " +
                                                                           "@ContractStatusTypeExternalRef = {6}, " +
                                                                           "@AdvisorExternalRef = {7}, " +
                                                                           "@ContractStartDate = {8}, " +
                                                                           "@ContractEndDate = {9}, " +
                                                                           "@DeliveryBeginStartDate = {10}, " +
                                                                           "@DeliveryBeginEndDate = {11}, " +
                                                                           "@DeliveryEndStartDate = {12}, " +
                                                                           "@DeliveryEndEndDate = {13}",
                                                                           contractTransactionTypeExternalRef,
                                                                           locationExternalRef,
                                                                           commodityExternalRef,
                                                                           customerName,
                                                                           marketZoneExternalRef,
                                                                           contractTypeExternalRef,
                                                                           contractStatusTypeExternalRef,
                                                                           advisorExternalRef,
                                                                           contractStartDate,
                                                                           contractEndDate,
                                                                           deliveryBeginStartDate,
                                                                           deliveryBeginEndDate,
                                                                           deliveryEndStartDate,
                                                                           deliveryEndEndDate)
                                                                           .ToListAsync();
        }

        [Obsolete]
        public async Task<List<ContractPricing>> GetContractPricings(int contractNumber)
        {
            return await _context.Query<ContractPricing>().FromSqlRaw("Execute dbo.GetContractPricings @ContractNumber = {0}", contractNumber).ToListAsync();
        }

        [Obsolete]
        public async Task<List<ContractAmendment>> GetContractAmendments(int contractNumber)
        {
            return await _context.Query<ContractAmendment>().FromSqlRaw("Execute dbo.GetContractAmendments @ContractNumber = {0}", contractNumber).ToListAsync();
        }

        public async Task<List<OfferSearchResult>> SearchOffers(int? contractTransactionTypeID, int? locationID, int? commodityID, string customerName, int? contractTypeID, int? offerStatusTypeID,
                                                                int? marketZoneID, int? advisorID, DateTime? offerStartDate, DateTime? offerEndDate, DateTime? deliveryBeginStartDate,
                                                                DateTime? deliveryBeginEndDate, DateTime? deliveryEndStartDate, DateTime? deliveryEndEndDate)
        {
            return await (from c in _context.Contracts
                          join cd in _context.ContractDetails on c.ContractID equals cd.ContractID
                          join a in _context.Accounts on cd.AccountID equals a.AccountID
                          join cmd in _context.Commodities on cd.CommodityID equals cmd.CommodityID
                          join ct in _context.ContractTypes on cd.ContractTypeID equals ct.ContractTypeID
                          join l in _context.Locations on cd.LocationID equals l.LocationID
                          join ctt in _context.ContractTransactionTypes on c.ContractTransactionTypeID equals ctt.ContractTransactionTypeID
                          join ost in _context.OfferStatusTypes on cd.OfferStatusTypeID equals ost.OfferStatusTypeID
                          join m in _context.MarketZones on cd.MarketZoneID equals m.MarketZoneID
                          join ad in _context.Advisors on cd.AdvisorID equals ad.AdvisorID
                          where cd.Offer.Value == true &&
                                (!contractTransactionTypeID.HasValue || c.ContractTransactionTypeID == contractTransactionTypeID.Value) &&
                                (!locationID.HasValue || cd.LocationID == locationID.Value) &&
                                (!commodityID.HasValue || cd.CommodityID == commodityID.Value) &&
                                (string.IsNullOrEmpty(customerName) || a.AccountName.Contains(customerName) || a.ExternalRef.Contains(customerName)) &&
                                (!contractTypeID.HasValue || cd.ContractTypeID == contractTypeID.Value) &&
                                (!offerStatusTypeID.HasValue || cd.OfferStatusTypeID == offerStatusTypeID) &&
                                (!marketZoneID.HasValue || cd.MarketZoneID == marketZoneID) &&
                                (!advisorID.HasValue || cd.AdvisorID == advisorID) &&
                                (
                                    (!offerStartDate.HasValue || !offerEndDate.HasValue)
                                    ||
                                    (offerStartDate.Value <= cd.ContractDetailOfferDate.Value && offerEndDate >= cd.ContractDetailOfferDate.Value)
                                )
                                &&
                                (
                                    (!deliveryBeginStartDate.HasValue || !deliveryBeginEndDate.HasValue)
                                    ||
                                    (deliveryBeginStartDate.Value <= cd.DeliveryStartDate.Value && deliveryBeginEndDate >= cd.DeliveryStartDate.Value)
                                )
                                &&
                                (
                                    (!deliveryEndStartDate.HasValue || !deliveryEndEndDate.HasValue)
                                    ||
                                    (deliveryEndStartDate.Value <= cd.DeliveryEndDate.Value && deliveryEndEndDate >= cd.DeliveryEndDate.Value)
                                )
                          orderby c.ContractID
                          select new OfferSearchResult
                          {
                              AccountID = a.ExternalRef,
                              AccountName = a.AccountName,
                              Basis = cd.OfferBasis,
                              CashPrice = cd.OfferCashPrice,
                              CommodityID = cd.CommodityID,
                              CommodityName = cmd.CommodityName,
                              ContractDate = cd.ContractDetailOfferDate.HasValue ? cd.ContractDetailOfferDate.Value.ToString("MM/dd/yyyy") : "",
                              ContractDateTime = cd.ContractDetailOfferDate,
                              ContractID = c.ContractID,
                              ContractNumber = c.ContractNumber,
                              ContractTypeID = cd.ContractTypeID,
                              ContractTypeName = ct.ContractTypeCode,
                              DeliveryEndDate = cd.DeliveryEndDate.HasValue ? cd.DeliveryEndDate.Value.ToString("MM/dd/yyyy") : "",
                              DeliveryEnd = cd.DeliveryEndDate,
                              DeliveryStartDate = cd.DeliveryStartDate.HasValue ? cd.DeliveryStartDate.Value.ToString("MM/dd/yyyy") : "",
                              DeliveryStart = cd.DeliveryStartDate,
                              FuturesPrice = cd.OfferFutures,
                              LocationID = cd.LocationID.HasValue ? cd.LocationID.Value : 0,
                              LocationName = l.LocationName,
                              Quantity = cd.Quantity,
                              ContractTransactionType = ctt.Description,
                              OfferStatusType = ost.OfferStatusTypeDescription,
                              MarketZone = m.Description,
                              AdvisorName = ad.AdvisorName
                          }).Take(1000).ToListAsync();
        }

        public async Task<List<ContractOfferSearchResult>> GetOffersAndContracts(int accountId)
        {
            return await (from c in _context.Contracts
                          join cd in _context.ContractDetails on c.ContractID equals cd.ContractID
                          join a in _context.Accounts on cd.AccountID equals a.AccountID
                          join cmd in _context.Commodities on cd.CommodityID equals cmd.CommodityID
                          join ct in _context.ContractTypes on cd.ContractTypeID equals ct.ContractTypeID
                          join l in _context.Locations on cd.LocationID equals l.LocationID
                          join ctt in _context.ContractTransactionTypes on c.ContractTransactionTypeID equals ctt.ContractTransactionTypeID
                          join m in _context.MarketZones on cd.MarketZoneID equals m.MarketZoneID
                          join ad in _context.Advisors on cd.AdvisorID equals ad.AdvisorID
                          join cest in _context.ContractExportStatusTypes on cd.ContractExportStatusTypeID equals cest.ContractExportStatusTypeID
                          where cd.AccountID == accountId
                          orderby c.AddDate
                          select new ContractOfferSearchResult
                          {
                              IsOffer = cd.Offer.Value,
                              OMTransactionType = cd.Offer.Value ? "Offer" : "Contract",
                              ContractExportStatusTypeID = cest.ContractExportStatusTypeID,
                              ContractExportStatusTypeName = cest.ContractExportStatusTypeName,
                              AccountID = a.ExternalRef,
                              AccountName = a.AccountName,
                              Basis = cd.OfferBasis,
                              CashPrice = cd.OfferCashPrice,
                              CommodityID = cd.CommodityID,
                              CommodityName = cmd.CommodityName,
                              ContractDate = cd.Offer.Value ?
                                             (cd.ContractDetailOfferDate.HasValue ? cd.ContractDetailOfferDate.Value.ToString("MM/dd/yyyy") : "") :
                                             (cd.ContractDetailDate.HasValue ? cd.ContractDetailDate.Value.ToString("MM/dd/yyyy") : ""),
                              ContractDateTime = cd.Offer.Value ? cd.ContractDetailOfferDate : cd.ContractDetailDate,
                              ContractDetailID = cd.ContractDetailID,
                              ContractID = c.ContractID,
                              ContractNumber = c.ContractNumber,
                              ContractTypeID = cd.ContractTypeID,
                              ContractTypeName = ct.ContractTypeCode,
                              DeliveryEndDate = cd.DeliveryEndDate.HasValue ? cd.DeliveryEndDate.Value.ToString("MM/dd/yyyy") : "",
                              DeliveryEnd = cd.DeliveryEndDate,
                              DeliveryStartDate = cd.DeliveryStartDate.HasValue ? cd.DeliveryStartDate.Value.ToString("MM/dd/yyyy") : "",
                              DeliveryStart = cd.DeliveryStartDate,
                              FuturesPrice = cd.Offer.Value ? cd.OfferFutures : cd.Futures,
                              LocationID = cd.LocationID.HasValue ? cd.LocationID.Value : 0,
                              LocationName = l.LocationName,
                              Quantity = cd.Quantity,
                              ContractTransactionType = ctt.Description,
                              MarketZone = m.Description,
                              AdvisorName = ad.AdvisorName
                          }).Take(1000).ToListAsync();
        }

        public async Task<List<ContractOfferSearchResult>> SearchOffersAndContracts(int? contractTransactionTypeID, int? locationID, int? commodityID, string customerName, int? contractTypeID,
                                                                                    int? marketZoneID, int? advisorID, DateTime? createdStartDate, DateTime? createdEndDate,
                                                                                    DateTime? deliveryBeginStartDate, DateTime? deliveryBeginEndDate, DateTime? deliveryEndStartDate,
                                                                                    DateTime? deliveryEndEndDate)
        {
            return await (from c in _context.Contracts
                          join cd in _context.ContractDetails on c.ContractID equals cd.ContractID
                          join a in _context.Accounts on cd.AccountID equals a.AccountID
                          join cmd in _context.Commodities on cd.CommodityID equals cmd.CommodityID
                          join ct in _context.ContractTypes on cd.ContractTypeID equals ct.ContractTypeID
                          join l in _context.Locations on cd.LocationID equals l.LocationID
                          join ctt in _context.ContractTransactionTypes on c.ContractTransactionTypeID equals ctt.ContractTransactionTypeID
                          join m in _context.MarketZones on cd.MarketZoneID equals m.MarketZoneID
                          join ad in _context.Advisors on cd.AdvisorID equals ad.AdvisorID
                          join cest in _context.ContractExportStatusTypes on cd.ContractExportStatusTypeID equals cest.ContractExportStatusTypeID
                          where (!contractTransactionTypeID.HasValue || c.ContractTransactionTypeID == contractTransactionTypeID.Value) &&
                                (!locationID.HasValue || cd.LocationID == locationID.Value) &&
                                (!commodityID.HasValue || cd.CommodityID == commodityID.Value) &&
                                (string.IsNullOrEmpty(customerName) || a.AccountName.Contains(customerName) || a.ExternalRef.Contains(customerName)) &&
                                (!contractTypeID.HasValue || cd.ContractTypeID == contractTypeID.Value) &&
                                (!marketZoneID.HasValue || cd.MarketZoneID == marketZoneID) &&
                                (!advisorID.HasValue || cd.AdvisorID == advisorID) &&
                                (
                                    (!createdStartDate.HasValue || !createdEndDate.HasValue)
                                    ||
                                    (createdStartDate.Value <= cd.AddDate && createdEndDate >= cd.AddDate)
                                )
                                &&
                                (
                                    (!deliveryBeginStartDate.HasValue || !deliveryBeginEndDate.HasValue)
                                    ||
                                    (deliveryBeginStartDate.Value <= cd.DeliveryStartDate.Value && deliveryBeginEndDate >= cd.DeliveryStartDate.Value)
                                )
                                &&
                                (
                                    (!deliveryEndStartDate.HasValue || !deliveryEndEndDate.HasValue)
                                    ||
                                    (deliveryEndStartDate.Value <= cd.DeliveryEndDate.Value && deliveryEndEndDate >= cd.DeliveryEndDate.Value)
                                )
                          orderby c.AddDate
                          select new ContractOfferSearchResult
                          {
                              IsOffer = cd.Offer.Value,
                              OMTransactionType = cd.Offer.Value ? "Offer" : "Contract",
                              ContractExportStatusTypeID = cest.ContractExportStatusTypeID,
                              ContractExportStatusTypeName = cest.ContractExportStatusTypeName,
                              AccountID = a.ExternalRef,
                              AccountName = a.AccountName,
                              Basis = cd.OfferBasis,
                              CashPrice = cd.OfferCashPrice,
                              CommodityID = cd.CommodityID,
                              CommodityName = cmd.CommodityName,
                              ContractDate = cd.Offer.Value ?
                                             (cd.ContractDetailOfferDate.HasValue ? cd.ContractDetailOfferDate.Value.ToString("MM/dd/yyyy") : "") :
                                             (cd.ContractDetailDate.HasValue ? cd.ContractDetailDate.Value.ToString("MM/dd/yyyy") : ""),
                              ContractDateTime = cd.Offer.Value ? cd.ContractDetailOfferDate : cd.ContractDetailDate,
                              ContractDetailID = cd.ContractDetailID,
                              ContractID = c.ContractID,
                              ContractNumber = c.ContractNumber,
                              ContractTypeID = cd.ContractTypeID,
                              ContractTypeName = ct.ContractTypeCode,
                              DeliveryEndDate = cd.DeliveryEndDate.HasValue ? cd.DeliveryEndDate.Value.ToString("MM/dd/yyyy") : "",
                              DeliveryEnd = cd.DeliveryEndDate,
                              DeliveryStartDate = cd.DeliveryStartDate.HasValue ? cd.DeliveryStartDate.Value.ToString("MM/dd/yyyy") : "",
                              DeliveryStart = cd.DeliveryStartDate,
                              FuturesPrice = cd.Offer.Value ? cd.OfferFutures : cd.Futures,
                              LocationID = cd.LocationID.HasValue ? cd.LocationID.Value : 0,
                              LocationName = l.LocationName,
                              Quantity = cd.Quantity,
                              ContractTransactionType = ctt.Description,
                              MarketZone = m.Description,
                              AdvisorName = ad.AdvisorName
                          }).Take(1000).ToListAsync();
        }
    }
}
