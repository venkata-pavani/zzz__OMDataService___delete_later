using System;

namespace OMSDataService.DomainObjects.Models
{


    public class BarChartHistoryResult
    {
        public BarchartResponseStatus status { get; set; }
        public BarchartTickResponse[] results { get; set; }
    }
      
    public class BarchartResponseStatus
    {
        public int code { get; set; }
        public string message { get; set; }
    }

    public class BarchartTickResponse
    {
        public string symbol { get; set; }
        public DateTime timestamp { get; set; }
        public string tradingDay { get; set; }
        public string sessionCode { get; set; }
        public decimal tickPrice { get; set; }
        public int tickSize { get; set; }
    }


       public class BarchartSymbol
    {
        public string symbol { get; set; }
        public string monthCode { get; set; }
        public string hedgeYear { get; set; }
        public int commodityId { get; set; }
    }

    public class BarchartGetQuoteItem
    {
        public string symbol { get; set; }
        public string name { get; set; }
        public string dayCode { get; set; }
        public string serverTimestamp { get; set; }
        public string mode { get; set; }
        public decimal lastPrice { get; set; }
        public string tradeTimestamp { get; set; }
        public decimal netChange { get; set; }
        public decimal percentChange { get; set; }
        public string unitCode { get; set; }
        public decimal open { get; set; }
        public decimal high { get; set; }
        public decimal low { get; set; }
        public decimal? close { get; set; }
        public decimal numTrades { get; set; }
        public decimal dollarVolume { get; set; }
        public string flag { get; set; }
        public decimal volume { get; set; }
        public decimal previousVolume { get; set; }
    }

    public class BarchartGetQuoteResponse
    {
        public BarchartResponseStatus status { get; set; }
        public BarchartGetQuoteItem[] results { get; set; }
    }
}
