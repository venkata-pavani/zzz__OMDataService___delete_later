using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMSDataService.DomainObjects.Models
{
    public class MarketZone
    {
        [Key]
        public int MarketZoneID { get; set; }
        public string MarketZoneName { get; set; }
        public string Description { get; set; }
        public string ExternalRef { get; set; }
        public string ExternalRefName { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime AddDate { get; set; }
        public int AddUserID { get; set; }
        public DateTime ChgDate { get; set; }
        public int ChgUserID { get; set; }
    }
}
