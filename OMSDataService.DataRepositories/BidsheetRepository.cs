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
using Microsoft.Extensions.Configuration;

namespace OMSDataService.DataRepositories
{
    public class BidsheetRepository : IBidsheetRepository
    {
        private readonly IMapper _mapper;
        private ApiContext _context;
        private IConfiguration _configuration;

        public BidsheetRepository(ApiContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<List<Bidsheet>> GetBidsheets()
        {
            return await _context.Bidsheets.OrderByDescending(x => x.BidsheetID).ToListAsync();
        }

        public async Task<List<BidsheetSearchResult>> GetBidsheetsToRollOfferTo(int locationId, int commodityId, int marketZoneId, bool useRealTimeQuotes)
        {
            var bidsheets = await (from b in _context.Bidsheets
                                   join l in _context.Locations on b.LocationID equals l.LocationID
                                   join c in _context.Commodities on b.CommodityID equals c.CommodityID
                                   join m in _context.Months on b.MonthID equals m.MonthID
                                   where b.IsActive &&
                                   //b.LocationID == locationId &&
                                   l.MarketZoneID == marketZoneId &&
                                   b.CommodityID == commodityId
                                   orderby b.DeliveryBeginDate, b.DeliveryEndDate
                                   select new BidsheetSearchResult
                                   {
                                       Basis = b.Basis,
                                       BidsheetID = b.BidsheetID,
                                       CommodityID = c.CommodityID,
                                       CommodityName = c.CommodityName,
                                       DeliveryBeginDate = b.DeliveryBeginDate.ToString("MM/dd/yyyy"),
                                       DeliveryBegin = b.DeliveryBeginDate,
                                       DeliveryEndDate = b.DeliveryEndDate.ToString("MM/dd/yyyy"),
                                       DeliveryEnd = b.DeliveryEndDate,
                                       DeliveryPeriod = b.DeliveryPeriod,
                                       LocationID = l.LocationID,
                                       LocationName = l.LocationName,
                                       FutureMonthID = b.MonthID,
                                       FutureMonthYear = m.MonthName + " " + b.OptionYear,
                                       HasOffers = false,
                                       Symbol = c.TickerSymbol,
                                       OptionMonthCode = m.MonthCode,
                                       OptionYear = b.OptionYear,
                                       TickConversion = c.TickConversion,
                                       MarketZoneID = l.MarketZoneID ?? 0,
                                       CommoditySymbol = c.Symbol + m.MonthCode + b.OptionYear.ToString().Substring(3, 1)
                                   }).ToListAsync();

            var url = "";

            if (useRealTimeQuotes)
            {
                url = _configuration.GetValue<string>("BarChart_RealTimeQuotes_URL");
            }

            else
            {
                url = _configuration.GetValue<string>("BarChart_DelayedQuotes_URL");
            }

            var symbolCount = 0;

            if (bidsheets.Count > 0)
            {
                foreach (var bidsheet in bidsheets)
                {
                    if (!string.IsNullOrEmpty(bidsheet.Symbol))
                    {
                        bidsheet.BarchartSymbol = bidsheet.Symbol + bidsheet.OptionMonthCode + bidsheet.OptionYear.ToString().Remove(0, 2);

                        if (symbolCount >= 1)
                        {
                            url += ",";
                        }

                        url += bidsheet.BarchartSymbol;

                        ++symbolCount;
                    }
                }

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(url))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var result = JsonSerializer.Deserialize(apiResponse, typeof(BarchartGetQuoteResponse)) as BarchartGetQuoteResponse;

                        foreach (var bidsheet in bidsheets)
                        {
                            if (!string.IsNullOrEmpty(bidsheet.BarchartSymbol))
                            {
                                if (result != null && result.results != null && result.results.Length > 0)
                                {
                                    var quote = result.results.Where(r => r.symbol == bidsheet.BarchartSymbol).FirstOrDefault();

                                    if (quote != null)
                                    {
                                        bidsheet.FuturesPrice = quote.lastPrice * bidsheet.TickConversion.Value;
                                        bidsheet.FuturesChange = quote.netChange;
                                        bidsheet.CashPrice = quote.lastPrice * bidsheet.TickConversion.Value + bidsheet.Basis;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return bidsheets;
        }

        public async Task<BidsheetSearchResult> GetBidsheetWithFutureValues(int bidsheetID, bool useRealTimeQuotes)
        {
            var bidsheet = await (from b in _context.Bidsheets
                                  join l in _context.Locations on b.LocationID equals l.LocationID
                                  join c in _context.Commodities on b.CommodityID equals c.CommodityID
                                  join m in _context.Months on b.MonthID equals m.MonthID
                                  where b.BidsheetID == bidsheetID
                                  select new BidsheetSearchResult
                                  {
                                      Basis = b.Basis,
                                      BidsheetID = b.BidsheetID,
                                      CommodityID = c.CommodityID,
                                      CommodityName = c.CommodityName,
                                      DeliveryBeginDate = b.DeliveryBeginDate.ToString("MM/dd/yyyy"),
                                      DeliveryBegin = b.DeliveryBeginDate,
                                      DeliveryEndDate = b.DeliveryEndDate.ToString("MM/dd/yyyy"),
                                      DeliveryEnd = b.DeliveryEndDate,
                                      DeliveryPeriod = b.DeliveryPeriod,
                                      LocationID = l.LocationID,
                                      LocationName = l.LocationName,
                                      FutureMonthID = b.MonthID,
                                      FutureMonthYear = m.MonthName + " " + b.OptionYear,
                                      HasOffers = false,
                                      Symbol = c.TickerSymbol,
                                      OptionMonthCode = m.MonthCode,
                                      OptionYear = b.OptionYear,
                                      TickConversion = c.TickConversion,
                                      MarketZoneID = l.MarketZoneID ?? 0,
                                      CommoditySymbol = c.Symbol + m.MonthCode + b.OptionYear.ToString().Substring(3, 1)
                                  }).SingleOrDefaultAsync();

            if (bidsheet != null && !string.IsNullOrEmpty(bidsheet.Symbol))
            {
                var url = "";

                if (useRealTimeQuotes)
                {
                    url = _configuration.GetValue<string>("BarChart_RealTimeQuotes_URL");
                }

                else
                {
                    url = _configuration.GetValue<string>("BarChart_DelayedQuotes_URL");
                }

                bidsheet.BarchartSymbol = bidsheet.Symbol + bidsheet.OptionMonthCode + bidsheet.OptionYear.ToString().Remove(0, 2);

                url += bidsheet.BarchartSymbol;

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(url))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var result = JsonSerializer.Deserialize(apiResponse, typeof(BarchartGetQuoteResponse)) as BarchartGetQuoteResponse;
                        
                        if (result != null && result.results != null && result.results.Length > 0)
                        {
                            var quote = result.results.Where(r => r.symbol == bidsheet.BarchartSymbol).FirstOrDefault();

                            if (quote != null)
                            {
                                bidsheet.FuturesPrice = quote.lastPrice * bidsheet.TickConversion.Value;
                                bidsheet.FuturesChange = quote.netChange;
                                bidsheet.CashPrice = quote.lastPrice * bidsheet.TickConversion.Value + bidsheet.Basis;
                            }
                        } 
                    }
                }
            }

            return bidsheet;
        }

        public async Task<Bidsheet> GetBidsheet(int bidsheetId)
        {
            return await _context.Bidsheets.FirstOrDefaultAsync(c => c.BidsheetID == bidsheetId);
        }

        public async Task<Bidsheet> GetNewBidsheet()
        {
            var nowDate = DateTime.Now;
            var now = new DateTime(nowDate.Year, nowDate.Month, nowDate.Day, 0, 0, 0);

            return new Bidsheet()
            {
                BidsheetID = 0,
                LocationID = -1,
                CommodityID = -1,
                MonthID = -1,
                OptionYear = -1,
                DeliveryBeginDate = now,
                DeliveryEndDate = now,
                PreferredBasis = 0,
                PriceProtection = 0,
                AllowProducerView = true,
                FOB = 0,
                Margin = 0,
                IsActive = true
            };
        }

        public void AddBidsheet(Bidsheet item)
        {
            item.AddDate = item.ChgDate = DateTime.Now;

            _context.Bidsheets.Add(item);
            _context.SaveChanges();
        }

        public void UpdateBidsheet(Bidsheet item)
        {
            item.ChgDate = DateTime.Now;

            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<List<BidsheetSearchResult>> SearchBidsheets(int? locationId, int? commodityId, bool active, bool countHasOffers, bool countHasOffersByAccountOnly,
                                                                      int? accountID, bool useRealTimeQuotes)
        {
            var bidsheets = await (from b in _context.Bidsheets
                                   join l in _context.Locations on b.LocationID equals l.LocationID
                                   join c in _context.Commodities on b.CommodityID equals c.CommodityID
                                   join m in _context.Months on b.MonthID equals m.MonthID
                                   where (b.IsActive == active) &&
                                   (locationId == null || b.LocationID == locationId.Value) &&
                                   (commodityId == null || b.CommodityID == commodityId.Value)
                                   orderby l.LocationName, c.CommodityName, b.DeliveryBeginDate, b.DeliveryEndDate
                                   select new BidsheetSearchResult
                                   {
                                       Basis = b.Basis,
                                       BidsheetID = b.BidsheetID,
                                       CommodityID = c.CommodityID,
                                       CommodityName = c.CommodityName,
                                       DeliveryBeginDate = b.DeliveryBeginDate.ToString("MM/dd/yyyy"),
                                       DeliveryBegin = b.DeliveryBeginDate,
                                       DeliveryEndDate = b.DeliveryEndDate.ToString("MM/dd/yyyy"),
                                       DeliveryEnd = b.DeliveryEndDate,
                                       DeliveryPeriod = b.DeliveryPeriod,
                                       LocationID = l.LocationID,
                                       LocationName = l.LocationName,
                                       FutureMonthID = b.MonthID,
                                       FutureMonthYear = m.MonthName + " " + b.OptionYear,
                                       HasOffers = false,
                                       Symbol = c.TickerSymbol,
                                       OptionMonthCode = m.MonthCode,
                                       OptionYear = b.OptionYear,
                                       TickConversion = c.TickConversion,
                                       MarketZoneID = l.MarketZoneID ?? 0,
                                       CommoditySymbol = c.Symbol + m.MonthCode + b.OptionYear.ToString().Substring(3, 1)
                                   }).ToListAsync();

            var url = "";

            if (useRealTimeQuotes)
            {
                url = _configuration.GetValue<string>("BarChart_RealTimeQuotes_URL");
            }

            else
            {
                url = _configuration.GetValue<string>("BarChart_DelayedQuotes_URL");
            }

            var symbolCount = 0;

            if (bidsheets.Count > 0)
            {
                foreach (var bidsheet in bidsheets)
                {
                    if (!string.IsNullOrEmpty(bidsheet.Symbol))
                    {
                        bidsheet.BarchartSymbol = bidsheet.Symbol + bidsheet.OptionMonthCode + bidsheet.OptionYear.ToString().Remove(0, 2);

                        if (symbolCount >= 1)
                        {
                            url += ",";
                        }

                        url += bidsheet.BarchartSymbol;

                        ++symbolCount;
                    }
                }

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(url))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var result = JsonSerializer.Deserialize(apiResponse, typeof(BarchartGetQuoteResponse)) as BarchartGetQuoteResponse;

                        foreach (var bidsheet in bidsheets)
                        {
                            if (!string.IsNullOrEmpty(bidsheet.BarchartSymbol))
                            {
                                if (result != null && result.results != null && result.results.Length > 0)
                                { 
                                    var quote = result.results.Where(r => r.symbol == bidsheet.BarchartSymbol).FirstOrDefault();

                                    if (quote != null)
                                    {
                                        bidsheet.FuturesPrice = quote.lastPrice * bidsheet.TickConversion.Value;
                                        bidsheet.FuturesChange = quote.netChange;
                                        bidsheet.CashPrice = quote.lastPrice * bidsheet.TickConversion.Value + bidsheet.Basis;
                                    }
                                }
                            }

                            if (countHasOffers)
                            {
                                if (countHasOffersByAccountOnly)
                                {
                                    bidsheet.HasOffers = _context.ContractDetails.Where(c => c.BidsheetID == bidsheet.BidsheetID && c.AccountID == accountID && c.Offer.Value).Count() > 0;
                                }

                                else
                                {
                                    bidsheet.HasOffers = _context.ContractDetails.Where(c => c.BidsheetID == bidsheet.BidsheetID && c.Offer.Value).Count() > 0;
                                }
                            }
                        }
                    }
                }
            }

            return bidsheets;
        }

        public async Task<List<BidsheetSearchResult>> SearchBidsheetsArchive(int? locationId, int? commodityId, DateTime? archiveStartDate, DateTime? archiveEndDate)
        {
            return await (from b in _context.BidsheetsHistory
                          join l in _context.Locations on b.LocationID equals l.LocationID
                          join c in _context.Commodities on b.CommodityID equals c.CommodityID
                          join m in _context.Months on b.MonthID equals m.MonthID
                          where (locationId == null || b.LocationID == locationId.Value) &&
                          (commodityId == null || b.CommodityID == commodityId.Value) &&
                          (
                            (!archiveStartDate.HasValue || !archiveEndDate.HasValue)
                            ||
                            (archiveStartDate.Value.Date <= b.ArchiveDate.Value.Date && archiveEndDate.Value.Date >= b.ArchiveDate.Value.Date)
                          )
                          orderby l.LocationName, c.CommodityName, b.DeliveryBeginDate, b.DeliveryEndDate
                          select new BidsheetSearchResult
                          {
                              Basis = b.Basis.Value,
                              BidsheetID = b.BidsheetID.Value,
                              CommodityID = c.CommodityID,
                              CommodityName = c.CommodityName,
                              DeliveryBeginDate = b.DeliveryBeginDate.Value.ToString("MM/dd/yyyy"),
                              DeliveryBegin = b.DeliveryBeginDate.Value,
                              DeliveryEndDate = b.DeliveryEndDate.Value.ToString("MM/dd/yyyy"),
                              DeliveryEnd = b.DeliveryEndDate.Value,
                              DeliveryPeriod = b.DeliveryPeriod,
                              LocationID = l.LocationID,
                              LocationName = l.LocationName,
                              FutureMonthID = b.MonthID.Value,
                              FutureMonthYear = m.MonthName + " " + b.OptionYear,
                              HasOffers = false,
                              Symbol = c.TickerSymbol,
                              OptionMonthCode = m.MonthCode,
                              OptionYear = b.OptionYear.Value,
                              TickConversion = c.TickConversion,
                              MarketZoneID = l.MarketZoneID ?? 0,
                              CommoditySymbol = c.Symbol + m.MonthCode + b.OptionYear.ToString().Substring(3, 1),
                              ArchiveDate = b.ArchiveDate.Value
                          }).Take(1000).ToListAsync();
        }
    }
}
