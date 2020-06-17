using System;

namespace OMSDataService.DomainObjects.Models
{
    public class BidsheetSearchResult
    {
        public int BidsheetID { get; set; }
        public string FacilityName { get; set; }
        public string CommodityName { get; set; }
        public string Symbol { get; set; }
        public string FutureMonthYear { get; set; }
        public int OptionYear { get; set; }
        public string OptionMonthCode { get; set; }
        public string DeliveryBeginDate { get; set; }
        public string DeliveryEndDate { get; set; }
        public decimal FuturesPrice { get; set; }
        public decimal FuturesChange { get; set; }
        public decimal Basis { get; set; }
        public decimal CashPrice { get; set; }
        public bool HasOffers { get; set; }
        public decimal Quantity { get; set; }
        public string BarchartSymbol { get; set; }
    }
}
