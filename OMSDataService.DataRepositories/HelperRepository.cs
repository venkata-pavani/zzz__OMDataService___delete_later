using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OMSDataService.DataInterfaces;
using OMSDataService.DomainObjects.Models;
using OMSDataService.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OMSDataService.DataRepositories
{
    public class HelperRepository : IHelperRepository
    {
        private readonly IMapper _mapper;
        private ApiContext _context;

        public HelperRepository(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ContractDetail>> GetTicksForOffers()
        {
            var cond = new List<ContractDetail>();

            var offerSymbols = (from cd in _context.ContractDetails
                                join c in _context.Commodities on cd.CommodityID equals c.CommodityID
                                join m in _context.Months on cd.HedgeMonthID equals m.MonthID
                                where cd.Offer == true && cd.OfferGoodUntilDate >= DateTime.Now && cd.OfferStatusTypeID == 1 && (cd.ContractTypeID == 1 || cd.ContractTypeID == 3)
                                select new BarchartSymbol
                                {
                                    symbol = c.Symbol + m.MonthCode + cd.HedgeYear.ToString().Substring(2, 2),
                                    monthCode = m.MonthCode,
                                    hedgeYear = cd.HedgeYear.ToString().Substring(2, 2)

                                }).Distinct().ToList();


            foreach (var o in offerSymbols)
            {
                var tickList = new List<TickHistory>();
                var url = "https://ondemand.websol.barchart.com/getHistory.json?apikey=061bdbf8ef8efcf5da6e335be86fa8de&symbol=";
                var tickSymbol = "Z" + o.symbol; // + o.monthCode + o.hedgeYear;
                var tickDateStart = DateTime.Now.AddMinutes(-2).ToString("yyyyMMddHHmmss");
                var tickDateEnd = DateTime.Now.ToString("yyyyMMddHHmmss");
                url = url + tickSymbol + "&type=ticks&startDate=" + tickDateStart + "00" + "&endDate=" + tickDateEnd;
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(url))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var result = JsonSerializer.Deserialize(apiResponse, typeof(BarChartHistoryResult)) as BarChartHistoryResult;
                        
                        foreach (var r in result.results)
                        {
                            TickHistory tickHistory = new TickHistory();
                            tickHistory.Symbol = r.symbol;
                            tickHistory.TickDateTime = r.timestamp;
                            tickHistory.TickPrice = r.tickPrice;
                            // tickHistory.TickHistoryID = 55;
                            tickHistory.TradingDay = Convert.ToDateTime(r.tradingDay);

                            var item = _context.TickHistoryFutures.Where(th => th.Symbol == r.symbol && th.TickDateTime == r.timestamp
                            && th.TickPrice == r.tickPrice && th.TradingDay.ToString("yyyy-MM-dd") == r.tradingDay) as TickHistory;
                            
                            if (item == null)
                            {
                                

                                tickList.Add(tickHistory);
                            }
                        }
                        if (tickList.Count > 0)
                        {
                            var distinctTickList = tickList.Distinct().ToList();
                            IEnumerable<TickHistory> unique = tickList.Distinct();
                            HashSet<TickHistory> set = new HashSet<TickHistory>(tickList.Count);
                            var mylist = tickList.RemoveAll(x => !set.Add(x));
                            _context.TickHistoryFutures.AddRange(distinctTickList);
                            await _context.SaveChangesAsync();

                        }
                    }
                }
            } 

          

            return cond;
        }
    }
}
