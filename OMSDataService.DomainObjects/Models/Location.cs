using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMSDataService.DomainObjects.Models
{
    public class Location
    {
        [Key]
        public int LocationID { get; set; }
        public int? CompanyID { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? Zip { get; set; }
        public int? StateID { get; set; }
        public int? CountyID { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public string WebAddress { get; set; }
        public string Note { get; set; }
        public bool? ContractingActive { get; set; }
        public bool? IsUsingContractSeqNo { get; set; }
        public int? MarketZoneID { get; set; }
        public string ExternalRef { get; set; }
        public string ExternalRefName { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime AddDate { get; set; }
        public string AddUserID { get; set; }
        public DateTime ChgDate { get; set; }
        public string ChgUserID { get; set; }
    }
}
