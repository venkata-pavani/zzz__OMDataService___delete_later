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
                          where cd.AccountID == accountId && cd.Offer.Value == true && !c.Deleted
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
                              CommoditySymbol = cmd.Symbol + mo.MonthCode + cd.HedgeYear.ToString().Substring(3, 1),
                              StatusColor = ost.StatusColor
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
                          where c.ContractNumber == contractNumber.ToString() && cd.Offer.Value == true && !c.Deleted
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
                              CommoditySymbol = cmd.Symbol + mo.MonthCode + cd.HedgeYear.ToString().Substring(3, 1),
                              StatusColor = ost.StatusColor
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
                                !c.Deleted &&
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
                              CommoditySymbol = cmd.Symbol + mo.MonthCode + cd.HedgeYear.ToString().Substring(3, 1),
                              StatusColor = ost.StatusColor
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

        public async Task<ContractDTO> GetNewContract(bool isSalesContract, bool isOffer, BidsheetSearchResult bidsheet, int? contractTypeID, int accountID)
        {
            var nowDate = DateTime.Now;
            var now = new DateTime(nowDate.Year, nowDate.Month, nowDate.Day, 0, 0, 0);

            int? offerPriceTypeID = null;

            if (isOffer && contractTypeID.HasValue)
            {
                if (contractTypeID == 1 || contractTypeID == 7)
                {
                    offerPriceTypeID = 2;
                }

                else if (contractTypeID == 2)
                {
                    offerPriceTypeID = 1;
                }

                else if (contractTypeID == 3)
                {
                    offerPriceTypeID = 3;
                }
            }

            decimal? futuresOnAdd = null;
            DateTime? futuresOnAddDateTime = null;

            if (!isOffer && bidsheet != null && !isSalesContract && contractTypeID.HasValue && (contractTypeID == 1 || contractTypeID == 3))
            {
                futuresOnAdd = bidsheet.FuturesPrice;
                futuresOnAddDateTime = now;
            }

            decimal? basisOnAdd = null;
            DateTime? basisOnAddDateTime = null;

            if (!isOffer && bidsheet != null && !isSalesContract && contractTypeID.HasValue && contractTypeID == 2)
            {
                basisOnAdd = bidsheet.Basis;
                basisOnAddDateTime = now;
            }

            Location location = null;

            if (bidsheet != null)
            {
                location = await _context.Locations.Where(l => l.LocationID == bidsheet.LocationID).SingleOrDefaultAsync();
            }

            var dto = new ContractDTO()
            {
                Contract = new Contract()
                {
                    ContractDate = now,
                    ContractTransactionTypeID = isSalesContract ? 2 : 1,
                    Deleted = false
                },

                ContractDetail = new ContractDetail()
                {
                    AccountID = accountID,
                    Basis = (bidsheet != null && !isOffer) ? bidsheet.Basis : (decimal?)null,
                    BasisOnAdd = basisOnAdd,
                    BasisOnAddDateTime = basisOnAddDateTime,
                    BidsheetID = bidsheet != null ? bidsheet.BidsheetID : (int?) null,
                    CashPrice = (bidsheet != null && !isOffer) ? bidsheet.CashPrice : (decimal?) null,
                    CommodityID = bidsheet != null ? bidsheet.CommodityID : (int?) null,
                    ContractDetailDate = isOffer ? (DateTime?) null : now,
                    ContractDetailOfferDate = isOffer ? now : (DateTime?) null,
                    ContractExportStatusTypeID = 5,
                    ContractStatusTypeID = isOffer ? (int?) null : 4,
                    ContractTypeID = contractTypeID.HasValue ? contractTypeID.Value : (int?) null,
                    DeliveryEndDate = bidsheet != null ? bidsheet.DeliveryEnd : (DateTime?) null,
                    DeliveryStartDate = bidsheet != null ? bidsheet.DeliveryBegin : (DateTime?)null,
                    FinalPrice = false,
                    Futures = (bidsheet != null && !isOffer) ? bidsheet.FuturesPrice : (decimal?)null,
                    FuturesOnAdd = futuresOnAdd,
                    FuturesOnAddDateTime = futuresOnAddDateTime,
                    HedgeMonthID = bidsheet != null ? bidsheet.FutureMonthID : (int?) null,
                    HedgeYear = bidsheet != null ? bidsheet.OptionYear : (int?) null,
                    IsArchived = false,
                    LocationID = location != null ? location.ContractingLocationID : (int?) null,
                    MarketZoneID = bidsheet != null ? bidsheet.MarketZoneID : (int?) null,
                    Offer = isOffer,
                    OfferBasis = (bidsheet != null && isOffer) ? bidsheet.Basis : (decimal?)null,
                    OfferCashPrice = (bidsheet != null && isOffer) ? bidsheet.CashPrice : (decimal?)null,
                    OfferDurationTypeID = isOffer ? 1 : (int?) null,
                    OfferFutures = (bidsheet != null && isOffer) ? bidsheet.FuturesPrice : (decimal?)null,
                    OfferPriceTypeID = offerPriceTypeID,
                    OfferStatusTypeID = isOffer ? 1 : (int?) null,
                    OfferTypeID = isOffer ? 2 : (int?) null,
                    Printed = false,
                    WasOffer = isOffer,
                    Deleted = false
                }
            };

            return dto;
        }

        [Obsolete]
        public async Task<ContractDTO> GetNewOfferFromContract(int contractNumber)
        {
            var contracts = await _context.Query<ContractSearchResult>().FromSqlRaw("Execute dbo.GetContract @ContractNumber = {0}", contractNumber).ToListAsync();

            if (contracts.Count == 1)
            {
                var contract = contracts[0];

                var nowDate = DateTime.Now;
                var now = new DateTime(nowDate.Year, nowDate.Month, nowDate.Day, 0, 0, 0);

                Month month = null;

                int? year = null;

                if (contract.OptionMonth != null && contract.OptionMonth.Length > 2)
                {
                    var monthCode = contract.OptionMonth[1].ToString();

                    month = await _context.Months.Where(m => m.MonthCode == monthCode).SingleOrDefaultAsync();

                    year = int.Parse(DateTime.Now.Year.ToString().Substring(0, 3) + contract.OptionMonth[2].ToString());
                }

                var dto = new ContractDTO()
                {
                    Contract = new Contract()
                    {
                        ContractDate = now,
                        ContractNumber = contract.ContractNumber,
                        ContractPricingStatusTypeID = contract.ContractPricingStatusTypeID,
                        ContractTransactionTypeID = 1,
                        Deleted = false
                    },

                    ContractDetail = new ContractDetail()
                    {
                        AccountID = contract.OMSAccountID,
                        CommodityID = contract.CommodityID.Value,
                        ContractDetailOfferDate = now,
                        ContractExportStatusTypeID = 5,
                        ContractTypeID = null,
                        DeliveryStartDate = contract.DeliveryStart,
                        DeliveryEndDate = contract.DeliveryEnd,
                        FinalPrice = false,
                        HedgeMonthID = month != null ? month.MonthID : (int?) null,
                        HedgeYear = year,
                        IsArchived = false,
                        LocationID = contract.LocationID,
                        MarketZoneID = contract.MarketZoneID,
                        Offer = true,
                        OfferDurationTypeID = 1,
                        OfferStatusTypeID = 1,
                        OfferTypeID = 2,
                        Quantity = contract.Quantity,
                        Printed = false,
                        WasOffer = true,
                        Deleted = false
                    }
                };

                return dto;
            }

            else
            {
                return new ContractDTO()
                {
                    Contract = new Contract(),
                    ContractDetail = new ContractDetail()
                };
            }
        }

        [Obsolete]
        public async Task<ContractDTO> GetNewPricingFromContract(int contractNumber)
        {
            var contracts = await _context.Query<ContractSearchResult>().FromSqlRaw("Execute dbo.GetContract @ContractNumber = {0}", contractNumber).ToListAsync();

            if (contracts.Count == 1)
            {
                var contract = contracts[0];

                var nowDate = DateTime.Now;
                var now = new DateTime(nowDate.Year, nowDate.Month, nowDate.Day, 0, 0, 0);

                Month month = null;

                int? year = null;

                if (contract.OptionMonth != null && contract.OptionMonth.Length > 2)
                {
                    var monthCode = contract.OptionMonth[1].ToString();

                    month = await _context.Months.Where(m => m.MonthCode == monthCode).SingleOrDefaultAsync();

                    year = int.Parse(DateTime.Now.Year.ToString().Substring(0, 3) + contract.OptionMonth[2].ToString());
                }

                var dto = new ContractDTO()
                {
                    Contract = new Contract()
                    {
                        ContractDate = now,
                        ContractNumber = contract.ContractNumber,
                        ContractPricingStatusTypeID = contract.ContractPricingStatusTypeID,
                        ContractTransactionTypeID = 1,
                        Deleted = false
                    },

                    ContractDetail = new ContractDetail()
                    {
                        AccountID = contract.OMSAccountID,
                        CommodityID = contract.CommodityID.Value,
                        ContractDetailDate = now,
                        ContractExportStatusTypeID = 5,
                        ContractStatusTypeID = 4,
                        ContractTypeID = null,
                        DeliveryStartDate = contract.DeliveryStart,
                        DeliveryEndDate = contract.DeliveryEnd,
                        FinalPrice = false,
                        HedgeMonthID = month != null ? month.MonthID : (int?) null,
                        HedgeYear = year,
                        IsArchived = false,
                        LocationID = contract.LocationID,
                        MarketZoneID = contract.MarketZoneID,
                        Quantity = contract.Quantity,
                        Printed = false,
                        WasOffer = false,
                        Deleted = false
                    }
                };

                return dto;
            }

            else
            {
                return new ContractDTO()
                {
                    Contract = new Contract(),
                    ContractDetail = new ContractDetail()
                };
            }
        }

        public async Task<ContractDTO> GetNewOfferClone(int contractID)
        {
            var contract = await _context.Contracts.Where(c => c.ContractID == contractID).SingleOrDefaultAsync();
            var contractDetail = await _context.ContractDetails.Where(c => c.ContractID == contractID).SingleOrDefaultAsync();

            if (contract != null && contractDetail != null)
            {
                contract.ContractID = 0;
                contractDetail.ContractID = 0;
                contractDetail.ContractDetailID = 0;
                contractDetail.ContractExportStatusTypeID = 5;
                contract.Deleted = contractDetail.Deleted = false;

                return new ContractDTO()
                {
                    Contract = contract,
                    ContractDetail = contractDetail
                };
            }

            else
            {
                return new ContractDTO()
                {
                    Contract = new Contract(),
                    ContractDetail = new ContractDetail()
                };
            }
        }

        public async Task<ContractDTO> GetNewContractClone(int contractID)
        {
            var contract = await _context.Contracts.Where(c => c.ContractID == contractID).SingleOrDefaultAsync();
            var contractDetail = await _context.ContractDetails.Where(c => c.ContractID == contractID).SingleOrDefaultAsync();

            if (contract != null && contractDetail != null)
            {
                contract.ContractID = 0;
                contract.ContractNumber = "";
                contractDetail.ContractID = 0;
                contractDetail.ContractDetailID = 0;
                contractDetail.ContractExportStatusTypeID = 5;
                contract.Deleted = contractDetail.Deleted = false;

                return new ContractDTO()
                {
                    Contract = contract,
                    ContractDetail = contractDetail
                };
            }

            else
            {
                return new ContractDTO()
                {
                    Contract = new Contract(),
                    ContractDetail = new ContractDetail()
                };
            }
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

            // capture trading slippage
            if (contract.ContractTransactionTypeID == 1 && (contractDetail.ContractTypeID == 1 || contractDetail.ContractTypeID == 2 || contractDetail.ContractTypeID == 3) &&
                !contractDetail.Offer.Value && contractDetail.BidsheetID != null)
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

                                if (contractDetail.ContractTypeID == 1 || contractDetail.ContractTypeID == 3)
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

        public void DeleteContract(Contract contract, ContractDetail contractDetail)
        {
            contract.ChgDate = contractDetail.ChgDate = DateTime.Now;
            contract.Deleted = contractDetail.Deleted = true;

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
                          where cd.Offer.Value == true && !c.Deleted &&
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
                              CommoditySymbol = cmd.Symbol + mo.MonthCode + cd.HedgeYear.ToString().Substring(3, 1),
                              StatusColor = ost.StatusColor
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
                          where cd.AccountID == accountId && !c.Deleted
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
                                        select cst).SingleOrDefault().Description,
                              StatusColor = cd.Offer.Value ?
                                            (from ost in _context.OfferStatusTypes
                                             where ost.OfferStatusTypeID == cd.OfferStatusTypeID
                                             select ost).SingleOrDefault().StatusColor
                                            :
                                            ""
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
                          where !c.Deleted &&
                                (!contractTransactionTypeID.HasValue || c.ContractTransactionTypeID == contractTransactionTypeID.Value) &&
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
                                        select cst).SingleOrDefault().Description,
                              StatusColor = cd.Offer.Value ?
                                            (from ost in _context.OfferStatusTypes
                                             where ost.OfferStatusTypeID == cd.OfferStatusTypeID
                                             select ost).SingleOrDefault().StatusColor
                                            :
                                            ""
                          }).Take(1000).ToListAsync();
        }

        public async Task<List<OfferSearchResult>> SearchPositionManagerOffers(int? commodityID, string commoditySymbol, string customerName, int? marketZoneID, int? advisorID)
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
                          join b in _context.Bidsheets on cd.BidsheetID equals b.BidsheetID
                          where cd.Offer.Value == true && !c.Deleted &&
                                //(cd.ContractTypeID == 1 || cd.ContractTypeID == 3) &&
                                (cd.OfferPriceTypeID == 2 || cd.OfferPriceTypeID == 3) &&
                                (cd.OfferStatusTypeID == 1 || cd.OfferStatusTypeID == 5) &&
                                c.ContractTransactionTypeID == 1 && 
                                (!commodityID.HasValue || cd.CommodityID == commodityID.Value) &&
                                (string.IsNullOrEmpty(customerName) || a.AccountName.Replace(" ", "").StartsWith(customerNameSearchString) || a.ExternalRef.Replace(" ", "").StartsWith(customerNameSearchString)) &&
                                (!marketZoneID.HasValue || cd.MarketZoneID == marketZoneID) &&
                                (!advisorID.HasValue || cd.AdvisorID == advisorID) &&
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
                              FuturesPrice = cd.ContractTypeID == 1 ? (cd.OfferCashPrice - b.Basis) : cd.OfferFutures,
                              LocationID = cd.LocationID.HasValue ? cd.LocationID.Value : 0,
                              LocationName = l.LocationName,
                              Quantity = cd.Quantity,
                              ContractTransactionType = ctt.Description,
                              OfferStatusType = ost.OfferStatusTypeDescription,
                              MarketZone = m.Description,
                              AdvisorName = ad.AdvisorName,
                              CommoditySymbol = cmd.Symbol + mo.MonthCode + cd.HedgeYear.ToString().Substring(3, 1),
                              StatusColor = ost.StatusColor
                          }).Take(1000).ToListAsync();
        }
    }
}
