using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OMSDataService.DataInterfaces;
using OMSDataService.DomainObjects.Models;
using OMSDataService.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

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
                          join mo in _context.Months on cd.HedgeMonthID equals mo.MonthID
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
                              AdvisorName = ad.AdvisorName,
                              CommoditySymbol = cmd.Symbol + mo.MonthCode + cd.HedgeYear.ToString().Substring(3, 1)
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
                          join mo in _context.Months on cd.HedgeMonthID equals mo.MonthID
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
                              AdvisorName = ad.AdvisorName,
                              CommoditySymbol = cmd.Symbol + mo.MonthCode + cd.HedgeYear.ToString().Substring(3, 1)
                          }).Take(1000).ToListAsync();
        }

        public async Task<List<OfferSearchResult>> GetOffersOnBidsheet(int bidsheetID, bool getOffersByAccountOnly, int? accountID)
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
                          join mo in _context.Months on cd.HedgeMonthID equals mo.MonthID
                          where cd.BidsheetID == bidsheetID &&
                                cd.Offer.Value == true &&
                                (!getOffersByAccountOnly || cd.AccountID == accountID)
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
                              AdvisorName = ad.AdvisorName,
                              CommoditySymbol = cmd.Symbol + mo.MonthCode + cd.HedgeYear.ToString().Substring(3, 1)
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

        public async Task<int> AddContract(Contract contract, ContractDetail contractDetail)
        {
            contract.AddDate = contractDetail.AddDate = contract.ChgDate = contractDetail.ChgDate = DateTime.Now;

            if (contractDetail.FuturesOnAddDateTime.HasValue)
            {
                contractDetail.FuturesOnAddDateTime = contractDetail.FuturesOnAddDateTime.Value.ToLocalTime();
            }

            if (contractDetail.BasisOnAddDateTime.HasValue)
            {
                contractDetail.BasisOnAddDateTime = contractDetail.BasisOnAddDateTime.Value.ToLocalTime();
            }

            if (contractDetail.AdvisorReviewDateTime.HasValue)
            {
                contractDetail.AdvisorReviewDateTime = contractDetail.AdvisorReviewDateTime.Value.ToLocalTime();
            }

            if (contractDetail.AdvisorManagerReviewDateTime.HasValue)
            {
                contractDetail.AdvisorManagerReviewDateTime = contractDetail.AdvisorManagerReviewDateTime.Value.ToLocalTime();
            }

            // if is purchase contract, then capture trading slippage
            if ((contract.ContractTransactionTypeID == 1 || contract.ContractTransactionTypeID == 2 || contract.ContractTransactionTypeID == 3) && !contractDetail.Offer.Value)
            {
                var commodity = _context.Commodities.SingleOrDefault(c => c.CommodityID == contractDetail.CommodityID);

                var month = _context.Months.SingleOrDefault(m => m.MonthID == contractDetail.HedgeMonthID.Value);

                var bidsheet = _context.Bidsheets.SingleOrDefault(b => b.BidsheetID == contractDetail.BidsheetID.Value);

                if (commodity != null && month != null && contractDetail.HedgeYear.HasValue && bidsheet != null)
                {
                    var symbol = commodity.TickerSymbol + month.MonthCode + contractDetail.HedgeYear.ToString().Remove(0, 2);

                    var url = "https://ondemand.websol.barchart.com/getQuote.json?apikey=061bdbf8ef8efcf5da6e335be86fa8de&symbols=" + symbol;

                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync(url))
                        {
                            var apiResponse = await response.Content.ReadAsStringAsync();

                            var result = JsonSerializer.Deserialize(apiResponse, typeof(BarchartGetQuoteResponse)) as BarchartGetQuoteResponse;

                            if (result != null && result.results != null && result.results.Length > 0)
                            {
                                var now = DateTime.Now;

                                if (contract.ContractTransactionTypeID == 1 || contract.ContractTransactionTypeID == 3)
                                {
                                    contractDetail.FuturesOnInsert = result.results[0].lastPrice * commodity.TickConversion.Value;
                                    contractDetail.FuturesOnInsertDateTime = now;
                                    contractDetail.BasisOnInsert = null;
                                    contractDetail.BasisOnInsertDateTime = null;
                                }
                                else
                                {
                                    contractDetail.FuturesOnInsert = null;
                                    contractDetail.FuturesOnInsertDateTime = null;
                                    contractDetail.BasisOnInsert = bidsheet.Basis;
                                    contractDetail.BasisOnInsertDateTime = now;
                                }
                            }
                        }
                    }
                }
            }
            
            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();

            contractDetail.ContractID = contract.ContractID;

            _context.ContractDetails.Add(contractDetail);
            await _context.SaveChangesAsync();

            return contract.ContractID;
        }

        public void UpdateContract(Contract contract, ContractDetail contractDetail)
        {
            contract.ChgDate = contractDetail.ChgDate = DateTime.Now;

            if (contractDetail.AdvisorReviewDateTime.HasValue)
            {
                contractDetail.AdvisorReviewDateTime = contractDetail.AdvisorReviewDateTime.Value.ToLocalTime();
            }

            if (contractDetail.AdvisorManagerReviewDateTime.HasValue)
            {
                contractDetail.AdvisorManagerReviewDateTime = contractDetail.AdvisorManagerReviewDateTime.Value.ToLocalTime();
            }

            _context.Entry(contract).State = EntityState.Modified;
            _context.Entry(contractDetail).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void ConvertOfferToContract(Contract contract, ContractDetail contractDetail)
        {
            contract.ChgDate = contractDetail.ChgDate = DateTime.Now;

            contractDetail.OfferStatusTypeID = 2;
            contractDetail.ContractDetailDate = DateTime.Now;
            contractDetail.ContractExportStatusTypeID = 1;
            contractDetail.Futures = contractDetail.OfferFutures;
            contractDetail.Basis = contractDetail.OfferBasis;
            contractDetail.CashPrice = contractDetail.OfferCashPrice;

            _context.Entry(contract).State = EntityState.Modified;
            _context.Entry(contractDetail).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void RollOffer(Contract contract, ContractDetail contractDetail, BidsheetSearchResult bidsheet)
        {
            contract.ChgDate = contractDetail.ChgDate = DateTime.Now;

            contractDetail.BidsheetID = bidsheet.BidsheetID;
            contractDetail.DeliveryStartDate = bidsheet.DeliveryBegin;
            contractDetail.DeliveryEndDate = bidsheet.DeliveryEnd;
            contractDetail.HedgeMonthID = bidsheet.FutureMonthID;
            contractDetail.HedgeYear = bidsheet.OptionYear;
            //contractDetail.OfferFutures = bidsheet.FuturesPrice;
            //contractDetail.OfferBasis = bidsheet.Basis;
            //contractDetail.OfferCashPrice = bidsheet.FuturesPrice + bidsheet.Basis;

            _context.Entry(contract).State = EntityState.Modified;
            _context.Entry(contractDetail).State = EntityState.Modified;
            _context.SaveChanges();
        }

        [Obsolete]
        public async Task<List<ContractSearchResult>> SearchContracts(string contractTransactionTypeExternalRef, string locationExternalRef, string commodityExternalRef, string commoditySymbol,
                                                                      string customerName, string marketZoneExternalRef, string contractTypeExternalRef, string contractStatusTypeExternalRef,
                                                                      string advisorExternalRef, DateTime? contractStartDate, DateTime? contractEndDate, DateTime? deliveryBeginStartDate,
                                                                      DateTime? deliveryBeginEndDate, DateTime? deliveryEndStartDate, DateTime? deliveryEndEndDate,
                                                                      string contractPricingStatusTypeExternalRef)
        {
            var customerNameSearchString = !string.IsNullOrEmpty(customerName) ? customerName.Replace(" ", "") : null;

            if (!string.IsNullOrEmpty(contractStatusTypeExternalRef))
            {
                if (contractStatusTypeExternalRef == "C")
                {
                    contractStatusTypeExternalRef = "Closed";
                }

                else if (contractStatusTypeExternalRef == "X")
                {
                    contractStatusTypeExternalRef = "Cancelled";
                }

                else
                {
                    contractStatusTypeExternalRef = "Open";
                }
            }

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
                                                                           "@DeliveryEndEndDate = {13}, " +
                                                                           "@CommoditySymbol = {14}, " +
                                                                           "@ContractPricingStatusType = {15}",
                                                                           contractTransactionTypeExternalRef,
                                                                           locationExternalRef,
                                                                           commodityExternalRef,
                                                                           customerNameSearchString,
                                                                           marketZoneExternalRef,
                                                                           contractTypeExternalRef,
                                                                           contractStatusTypeExternalRef,
                                                                           advisorExternalRef,
                                                                           contractStartDate,
                                                                           contractEndDate,
                                                                           deliveryBeginStartDate,
                                                                           deliveryBeginEndDate,
                                                                           deliveryEndStartDate,
                                                                           deliveryEndEndDate,
                                                                           commoditySymbol,
                                                                           contractPricingStatusTypeExternalRef)
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

        public async Task<List<OfferSearchResult>> SearchOffers(int? contractTransactionTypeID, int? locationID, int? commodityID, string commoditySymbol, string customerName,
                                                                int? contractTypeID, int? offerStatusTypeID, int? marketZoneID, int? advisorID, DateTime? offerStartDate,
                                                                DateTime? offerEndDate, DateTime? deliveryBeginStartDate, DateTime? deliveryBeginEndDate, DateTime? deliveryEndStartDate,
                                                                DateTime? deliveryEndEndDate)
        {
            var customerNameSearchString = !string.IsNullOrEmpty(customerName) ? customerName.Replace(" ", "") : "";

            var symbol = "";

            var monthCode = "";

            var hedgeYear = "";

            if (!string.IsNullOrEmpty(commoditySymbol))
            {
                if (commoditySymbol.Length >= 1)
                {
                    symbol = commoditySymbol.Substring(0, 1);
                }

                if (commoditySymbol.Length >= 2)
                {
                    monthCode = commoditySymbol.Substring(1, 1);
                }

                if (commoditySymbol.Length >= 3)
                {
                    hedgeYear = commoditySymbol.Substring(2, 1);
                }
            }

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
                          join mo in _context.Months on cd.HedgeMonthID equals mo.MonthID
                          where cd.Offer.Value == true &&
                                (!contractTransactionTypeID.HasValue || c.ContractTransactionTypeID == contractTransactionTypeID.Value) &&
                                (!locationID.HasValue || cd.LocationID == locationID.Value) &&
                                (!commodityID.HasValue || cd.CommodityID == commodityID.Value) &&
                                (string.IsNullOrEmpty(customerName) || a.AccountName.Replace(" ", "").StartsWith(customerNameSearchString) || a.ExternalRef.Replace(" ", "").StartsWith(customerNameSearchString)) &&
                                (!contractTypeID.HasValue || cd.ContractTypeID == contractTypeID.Value) &&
                                (!offerStatusTypeID.HasValue || cd.OfferStatusTypeID == offerStatusTypeID) &&
                                (!marketZoneID.HasValue || cd.MarketZoneID == marketZoneID) &&
                                (!advisorID.HasValue || cd.AdvisorID == advisorID) &&
                                (
                                    (!offerStartDate.HasValue || !offerEndDate.HasValue)
                                    ||
                                    (offerStartDate.Value.Date <= cd.ContractDetailOfferDate.Value.Date && offerEndDate.Value.Date >= cd.ContractDetailOfferDate.Value.Date)
                                )
                                &&
                                (
                                    (!deliveryBeginStartDate.HasValue || !deliveryBeginEndDate.HasValue)
                                    ||
                                    (deliveryBeginStartDate.Value.Date <= cd.DeliveryStartDate.Value.Date && deliveryBeginEndDate.Value.Date >= cd.DeliveryStartDate.Value.Date)
                                )
                                &&
                                (
                                    (!deliveryEndStartDate.HasValue || !deliveryEndEndDate.HasValue)
                                    ||
                                    (deliveryEndStartDate.Value.Date <= cd.DeliveryEndDate.Value.Date && deliveryEndEndDate.Value.Date >= cd.DeliveryEndDate.Value.Date)
                                )
                                &&
                                (string.IsNullOrEmpty(commoditySymbol) || (cmd.Symbol == symbol && mo.MonthCode == monthCode && cd.HedgeYear.ToString().Substring(3, 1) == hedgeYear))
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
                              AdvisorName = ad.AdvisorName,
                              CommoditySymbol = cmd.Symbol + mo.MonthCode + cd.HedgeYear.ToString().Substring(3, 1)
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
                          join mo in _context.Months on cd.HedgeMonthID equals mo.MonthID
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
                              AdvisorName = ad.AdvisorName,
                              CommoditySymbol = cmd.Symbol + mo.MonthCode + cd.HedgeYear.ToString().Substring(3, 1),
                              Status = cd.Offer.Value ?
                                       (from ost in _context.OfferStatusTypes
                                        where ost.OfferStatusTypeID == cd.OfferStatusTypeID
                                        select ost).SingleOrDefault().OfferStatusTypeDescription
                                        :
                                       (from cst in _context.ContractStatusTypes
                                        where cst.ContractStatusTypeID == cd.ContractStatusTypeID
                                        select cst).SingleOrDefault().Description
                          }).Take(1000).ToListAsync();
        }

        public async Task<List<ContractOfferSearchResult>> SearchOffersAndContracts(int? contractTransactionTypeID, int? locationID, int? commodityID, string commoditySymbol,
                                                                                    string customerName, int? contractTypeID, int? marketZoneID, int? advisorID, DateTime? createdStartDate,
                                                                                    DateTime? createdEndDate, DateTime? deliveryBeginStartDate, DateTime? deliveryBeginEndDate,
                                                                                    DateTime? deliveryEndStartDate, DateTime? deliveryEndEndDate)
        {
            var customerNameSearchString = !string.IsNullOrEmpty(customerName) ? customerName.Replace(" ", "") : "";

            var symbol = "";

            var monthCode = "";

            var hedgeYear = "";

            if (!string.IsNullOrEmpty(commoditySymbol))
            {
                if (commoditySymbol.Length >= 1)
                {
                    symbol = commoditySymbol.Substring(0, 1);
                }

                if (commoditySymbol.Length >= 2)
                {
                    monthCode = commoditySymbol.Substring(1, 1);
                }

                if (commoditySymbol.Length >= 3)
                {
                    hedgeYear = commoditySymbol.Substring(2, 1);
                }
            }

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
                          join mo in _context.Months on cd.HedgeMonthID equals mo.MonthID
                          where (!contractTransactionTypeID.HasValue || c.ContractTransactionTypeID == contractTransactionTypeID.Value) &&
                                (!locationID.HasValue || cd.LocationID == locationID.Value) &&
                                (!commodityID.HasValue || cd.CommodityID == commodityID.Value) &&
                                (string.IsNullOrEmpty(customerName) || a.AccountName.Replace(" ", "").StartsWith(customerNameSearchString) || a.ExternalRef.Replace(" ", "").StartsWith(customerNameSearchString)) &&
                                (!contractTypeID.HasValue || cd.ContractTypeID == contractTypeID.Value) &&
                                (!marketZoneID.HasValue || cd.MarketZoneID == marketZoneID) &&
                                (!advisorID.HasValue || cd.AdvisorID == advisorID) &&
                                (
                                    (!createdStartDate.HasValue || !createdEndDate.HasValue)
                                    ||
                                    (createdStartDate.Value.Date <= cd.AddDate.Date && createdEndDate.Value.Date >= cd.AddDate.Date)
                                )
                                &&
                                (
                                    (!deliveryBeginStartDate.HasValue || !deliveryBeginEndDate.HasValue)
                                    ||
                                    (deliveryBeginStartDate.Value.Date <= cd.DeliveryStartDate.Value.Date && deliveryBeginEndDate.Value.Date >= cd.DeliveryStartDate.Value.Date)
                                )
                                &&
                                (
                                    (!deliveryEndStartDate.HasValue || !deliveryEndEndDate.HasValue)
                                    ||
                                    (deliveryEndStartDate.Value.Date <= cd.DeliveryEndDate.Value.Date && deliveryEndEndDate.Value.Date >= cd.DeliveryEndDate.Value.Date)
                                )
                                &&
                                (string.IsNullOrEmpty(commoditySymbol) || (cmd.TickerSymbol == symbol && mo.MonthCode == monthCode && cd.HedgeYear.ToString().Substring(3, 1) == hedgeYear))
                          orderby c.AddDate
                          select new ContractOfferSearchResult
                          {
                              IsOffer = cd.Offer.Value,
                              OMTransactionType = cd.Offer.Value ? "Offer" : "Contract",
                              ContractExportStatusTypeID = cest.ContractExportStatusTypeID,
                              ContractExportStatusTypeName = cest.ContractExportStatusTypeName,
                              AccountID = a.ExternalRef,
                              AccountName = a.AccountName,
                              Basis = cd.Offer.Value ? cd.OfferBasis : cd.Basis,
                              CashPrice = cd.Offer.Value ? cd.OfferCashPrice : cd.CashPrice,
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
                              AdvisorName = ad.AdvisorName,
                              CommoditySymbol = cmd.Symbol + mo.MonthCode + cd.HedgeYear.ToString().Substring(3, 1),
                              Status = cd.Offer.Value ?
                                       (from ost in _context.OfferStatusTypes
                                        where ost.OfferStatusTypeID == cd.OfferStatusTypeID
                                        select ost).SingleOrDefault().OfferStatusTypeDescription
                                        :
                                       (from cst in _context.ContractStatusTypes
                                        where cst.ContractStatusTypeID == cd.ContractStatusTypeID
                                        select cst).SingleOrDefault().Description
                          }).Take(1000).ToListAsync();
        }
    }
}
