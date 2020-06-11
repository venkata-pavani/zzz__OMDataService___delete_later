using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMSDataService.DomainObjects.Models
{
    public class ContractDetail
    {
        [Key]
        public int? ContractDetailID { get; set; }
        public int? ContractID { get; set; }
        public string ContractTypeID { get; set; }
        public string ContractStatusID { get; set; }
        public string Description { get; set; }
        public string ContractLetter { get; set; }
        public int? FacilityID { get; set; }
        public int? BidsheetID { get; set; }
        public DateTime? DeliveryStartDate { get; set; }
        public DateTime? DeliveryEndDate { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? CashPrice { get; set; }
        public decimal? FuturesPrice { get; set; }
        public decimal? BasisPrice { get; set; }
        public int BatchNumber { get; set; }
        public decimal? RollBalance { get; set; }
        public string ExternalSystemRef { get; set; }
        public int CropID { get; set; }
        public int? HarvestDelivery { get; set; }
        public int HedgeTypeID { get; set; }
        public int HedgeOrderID { get; set; }
        public string HedgeOrderRef { get; set; }
        public string RemarksToAccount { get; set; }
        public string RemarksToAdvisor { get; set; }
        public bool? FinalPrice { get; set; }
        public bool? Printed { get; set; }
        public bool? IsArchived { get; set; }
        public DateTime? AddDate { get; set; }
        public int? AddUserID { get; set; }
        public DateTime? ChgDate { get; set; }
        public int? ChgUserID { get; set; }
    }
}
