using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMSDataService.DomainObjects.Models
{
    public class Commodity
    {
        [Key]
        public int CommodityID { get; set; }
        public string CommodityName { get; set; }
        public string CommodityCode { get; set; }
        public string TickerSymbol { get; set; }
        public decimal? TickConversion { get; set; }
        public decimal? ContractConversion { get; set; }
        public string ColorCode { get; set; }
        public decimal? DailyPriceLimit { get; set; }
        public int? HedgeCommodityID { get; set; }
        public int? ExchangeID { get; set; }
        public string ExternalRef { get; set; }
        public string ExternalRefName { get; set; }
        public bool? IsDefault { get; set; }
        public int? SortOrder { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? AddDate { get; set; }
        public int? AddUserID { get; set; }
        public DateTime? ChgDate { get; set; }
        public int? ChgUserID { get; set; }
    }
}
