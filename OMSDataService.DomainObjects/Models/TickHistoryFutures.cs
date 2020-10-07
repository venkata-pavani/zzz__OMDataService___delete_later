using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OMSDataService.DomainObjects.Models
{
    public class TickHistoryFutures
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TickHistoryID { get; set; }
        public string Symbol { get; set; }
        public DateTime TickDateTime { get; set; }
        public DateTime TradingDay { get; set; }
        public decimal TickPrice { get; set; }
        public int CommodityId { get; set; }
    }  
} 
 