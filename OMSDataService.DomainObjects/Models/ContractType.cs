using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMSDataService.DomainObjects.Models
{
    public class ContractType
    {
        [Key]
        public int ContractTypeID { get; set; }
        public string ContractTypeCode { get; set; }
        public string Description { get; set; }
        public bool PriceContract { get; set; }
        public bool BasisContract { get; set; }
        public bool FuturesContract { get; set; }
        public bool AvgContract { get; set; }
        public bool AllowCustomerAddContract { get; set; }
        public bool AllowCustomerAddOffer { get; set; }
        public string AccountingCode { get; set; }
        public string ExternalRef { get; set; }
        public string ExternalRefName { get; set; }
        public bool SendHedge { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime AddDate { get; set; }
        public string AddUserID { get; set; }
        public DateTime ChgDate { get; set; }
        public string ChgUserID { get; set; }
    }
}
