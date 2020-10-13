using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OMSDataService.DataInterfaces;
using OMSDataService.DomainObjects.Models;
using OMSDataService.EF;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<List<TickHistoryFutures>> GetTicksForOffers()
        {
            var tickList = new List<TickHistoryFutures>();

            TimeSpan start = new TimeSpan(7, 55, 0); //10 o'clock
            TimeSpan end = new TimeSpan(8, 40, 0); //12 o'clock

            TimeSpan startPM = new TimeSpan(13, 40, 0); //10 o'clock
            TimeSpan endPM = new TimeSpan(19, 11, 0); //12 o'clock
            TimeSpan now = DateTime.Now.TimeOfDay;

            var runProcess = true;

            if ((now > start && now < end))
            {
                runProcess = false;
            }

            if ((now > startPM && now < endPM))
            {
                runProcess = false;
            }

            if (runProcess == false)
            {
                return tickList;
            }

            SqlParameter param2;
            SqlParameter param3;
            SqlParameter param4;
            SqlParameter param5;
            SqlParameter param1;

            var tickDateStartTime = DateTime.Now.AddMinutes(-12);
            var tickDateStart = DateTime.Now.AddMinutes(-12).ToString("yyyyMMddhhmmss");




            var offerSymbols = (from cd in _context.ContractDetails
                                join c in _context.Commodities on cd.CommodityID equals c.CommodityID
                                join m in _context.Months on cd.HedgeMonthID equals m.MonthID
                                join ct in _context.ContractTypes on cd.ContractTypeID equals ct.ContractTypeID
                                where cd.Offer == true && cd.OfferGoodUntilDate >= DateTime.Now
                                && cd.OfferStatusTypeID == 1 && (ct.PriceContract || ct.FuturesContract)
                                select new BarchartSymbol
                                {
                                    symbol = c.Symbol + m.MonthCode + cd.HedgeYear.ToString().Substring(2, 2),
                                    monthCode = m.MonthCode,
                                    hedgeYear = cd.HedgeYear.ToString().Substring(2, 2),
                                    commodityId = c.CommodityID


                                }).Distinct().ToList();


            foreach (var o in offerSymbols)
            {
                var url = "https://ondemand.websol.barchart.com/getHistory.json?apikey=061bdbf8ef8efcf5da6e335be86fa8de&symbol=";
                var tickSymbol = "Z" + o.symbol; // + o.monthCode + o.hedgeYear;
                var tickDateEnd = DateTime.Now.AddMinutes(-10).ToString("yyyyMMddhhmmss");
                url = url + tickSymbol + "&type=ticks&startDate=" + tickDateStart + "&endDate=" + tickDateEnd;
                using (var httpClient = new HttpClient())
                {

                    using (var response = await httpClient.GetAsync(url))
                    {
                        if (response.IsSuccessStatusCode && !response.StatusCode.Equals(StatusCodes.Status204NoContent))
                        {
                            var apiResponse = await response.Content.ReadAsStringAsync();
                            if (apiResponse.Contains("No Content"))
                            {
                                return tickList;
                            }

                            var result = JsonSerializer.Deserialize(apiResponse, typeof(BarChartHistoryResult)) as BarChartHistoryResult;
                            foreach (var r in result.results)
                            {
                                TickHistoryFutures tickHistory = new TickHistoryFutures();
                                tickHistory.Symbol = r.symbol;
                                tickHistory.TickDateTime = r.timestamp;
                                tickHistory.TickPrice = r.tickPrice;
                                tickHistory.CommodityId = o.commodityId;
                                tickHistory.TradingDay = Convert.ToDateTime(r.tradingDay);


                                param2 = new SqlParameter("Symbol", tickHistory.Symbol);
                                param3 = new SqlParameter("TickPrice", tickHistory.TickPrice);
                                param4 = new SqlParameter("TickDateTime", tickHistory.TickDateTime);
                                param5 = new SqlParameter("TradeDate", tickHistory.TradingDay);
                                param1 = new SqlParameter("CommodityId", o.commodityId);


                                var ticketResult = _context.TickHistoryFutures.FromSqlRaw("TickHistoryFutures_Insert @Symbol, @TickPrice, @TickDateTime, @TradeDate, @CommodityID ", param2, param3, param4, param5, param1).ToList();

                            }
                        }
                    }
                }
            }

            MarkOffersHit(tickDateStartTime);

            return tickList;
        }



        private void MarkOffersHit(DateTime tickDateStart)
        {
            string sqlFormattedDate = tickDateStart.ToString("yyyy-MM-dd HH:mm:ss");
            SqlParameter param1;
            param1 = new SqlParameter("RequestTime", sqlFormattedDate);


            var ticketResult = _context.TickHistoryFutures.FromSqlRaw("MarkOffersHit @RequestTime ", param1).ToList();



        }

        private static bool isCurrentDateBetween(DateTime fromDate, DateTime toDate)
        {
            DateTime curent = DateTime.Now.Date;
            if (fromDate.CompareTo(toDate) >= 1)
            {
                return false;
            }
            int cd_fd = curent.CompareTo(fromDate);
            int cd_td = curent.CompareTo(toDate);

            if (cd_fd == 0 || cd_td == 0)
            {
                return true;
            }

            if (cd_fd >= 1 && cd_td <= -1)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Emails>> SendOfferEmailItems()
        {
           
          var emailList = _context.Emails.Where(x => x.Sent == false).ToList();

            foreach (var e in emailList)
            {
                if (e.EmailType == 2)
                {
                    //TwilioClient.Init(systemDefaults.TwilioAccountSID, systemDefaults.TwilioAuthToken);

                    //var twilioMessage = MessageResource.Create(
                    //    //to: new PhoneNumber("+16202424790"), // + user.PhoneNumber.ToString()),

                    //    to: new PhoneNumber("+1" + e.PhoneNumber.ToString()),
                    //    from: new PhoneNumber("+" + systemDefaults.TwilioPhoneNumber.ToString()),
                    //    body: e.EmailText);

                }
                if (e.EmailType == 1)
                {

                    var apiKey = "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";//Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
                    var client = new SendGridClient(apiKey);
                    var from = new EmailAddress("paige@notrs.com");
                    var subject = e.Subject;
                    var to = new EmailAddress("darron@notrs.com");
                    var plainTextContent = e.EmailText;
                    var htmlContent = e.EmailText;
                    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                    var response = await client.SendEmailAsync(msg);
                    var update = UpdateOfferEmail(e);

                }

            }


            return emailList;
        }

        private bool UpdateOfferEmail(Emails email)
        {
           
            email.Sent = true;
            _context.SaveChanges();

            return true;

        }
    }
}
