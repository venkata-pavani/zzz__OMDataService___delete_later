using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMSDataService.DomainObjects.Models
{
    public class Exchange
    {
        [Key]
        public int ExchangeID { get; set; }
        public string ExchangeName { get; set; }
        public string ExchangeCode { get; set; }
        public bool IsDefault { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime AddDate { get; set; }
        public int AddUserID { get; set; }
        public DateTime ChgDate { get; set; }
        public int ChgUserID { get; set; }
    }
}
