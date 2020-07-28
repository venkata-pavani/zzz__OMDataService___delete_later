using System;

namespace OMSDataService.DomainObjects.Models
{
    public class BarchartResponseStatus
    {
        public int code { get; set; }
        public string message { get; set; }
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
        public decimal close { get; set; }
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
