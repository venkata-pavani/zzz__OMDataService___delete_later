using System;
using System.ComponentModel.DataAnnotations;

namespace OMSDataService.DomainObjects.Models
{
    public class BidsheetsHistory
    {
        [Key]
        public int BidsheetHistoryID { get; set; }
        public DateTime? ArchiveDate { get; set; }
        public int? BidsheetID { get; set; }
        public int? LocationID { get; set; }
        public int? CommodityID { get; set; }
        public int? MonthID { get; set; }
        public int? OptionYear { get; set; }
        public string DeliveryPeriod { get; set; }
        public DateTime? DeliveryBeginDate { get; set; }
        public DateTime? DeliveryEndDate { get; set; }
        public decimal? Basis { get; set; }
        public decimal? PreferredBasis { get; set; }
        public decimal? PriceProtection { get; set; }
        public bool? AllowProducerView { get; set; }
        public decimal? FOB { get; set; }
        public decimal? Margin { get; set; }
        public decimal? FuturesPrice { get; set; }
        public decimal? FuturesHighPrice { get; set; }
        public decimal? FuturesLowPrice { get; set; }
        public decimal? FuturesClosingPrice { get; set; }
        public decimal? FuturesPreviousClosingPrice { get; set; }
        public string ExternalRef { get; set; }
        public bool IsActive { get; set; }
        public DateTime AddDate { get; set; }
        public string AddUserID { get; set; }
        public DateTime ChgDate { get; set; }
        public string ChgUserID { get; set; }
    }
}
