using System;

namespace OMSDataService.DomainObjects.Models
{
    public class BidsheetSearchResult
    {
        public int BidsheetID { get; set; }
        public string LocationName { get; set; }
        public string CommodityName { get; set; }
        public string Symbol { get; set; }
        public string FutureMonthYear { get; set; }
        public int OptionYear { get; set; }
        public string OptionMonthCode { get; set; }
        public string DeliveryBeginDate { get; set; }
        public string DeliveryEndDate { get; set; }
        public string FuturesPrice { get; set; }
        public string FuturesChange { get; set; }
        public string Basis { get; set; }
        public string CashPrice { get; set; }
        public bool HasOffers { get; set; }
        public decimal Quantity { get; set; }
        public string BarchartSymbol { get; set; }
        public decimal? TickConversion { get; set; }
        public string DeliveryPeriod { get; set; }
    }
}
