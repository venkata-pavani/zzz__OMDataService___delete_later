using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMSDataService.DomainObjects.Models
{
    public class Contract
    {
        [Key]
        public int ContractID { get; set; }
        public int? ContractStatusID { get; set; }
        public int? ContractTransactionTypeID { get; set; }
        public string ContractNumber { get; set; }
        public DateTime ContractDate { get; set; }
        public int AccountID { get; set; }
        public int LocationID { get; set; }
        public int? ContractPricingStatusTypeID { get; set; }
        public int? CropYearID { get; set; }
        public decimal Quantity { get; set; }
        public string RemarksToAccount { get; set; }
        public string RemarksToAdvisor { get; set; }
        public bool Printed { get; set; }
        public bool IsArchived { get; set; }
        public DateTime AddDate { get; set; }
        public string AddUserID { get; set; }
        public DateTime ChgDate { get; set; }
        public string ChgUserID { get; set; }
        public bool Deleted { get; set; }
    }
}
