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
        public int ContractStatusID { get; set; }
        public int ContractTransactionTypeID { get; set; }
        public string ContractNumber { get; set; }
        public int? AccountID { get; set; }
        public int FacilityID { get; set; }
        public int? CropYearID { get; set; }
        public decimal? Quantity { get; set; }
        public string RemarksToAccount { get; set; }
        public string RemarksToAdvisor { get; set; }
        public bool Printed { get; set; }
        public bool IsArchived { get; set; }
        public DateTime AddDate { get; set; }
        public int AddUserID { get; set; }
        public DateTime ChgDate { get; set; }
        public int ChgUserID { get; set; }
    }
}
