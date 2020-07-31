using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMSDataService.DomainObjects.Models
{
    public class Bidsheet

    {
        [Key]
        public int BidsheetID { get; set; }
        public int LocationID { get; set; }
        public int CommodityID { get; set; }
        public int MonthID { get; set; }
        public int OptionYear { get; set; }
        public string DeliveryPeriod { get; set; }
        public DateTime DeliveryBeginDate { get; set; }
        public DateTime DeliveryEndDate { get; set; }
        public decimal Basis { get; set; }
        public decimal PreferredBasis { get; set; }
        public decimal PriceProtection { get; set; }
        public bool AllowProducerView { get; set; }
        public decimal FOB { get; set; }
        public decimal Margin { get; set; }
        public string ExternalRef { get; set; }
        public bool IsActive { get; set; }
        public DateTime AddDate { get; set; }
        public string AddUserID { get; set; }
        public DateTime ChgDate { get; set; }
        public string ChgUserID { get; set; }
    }
}
