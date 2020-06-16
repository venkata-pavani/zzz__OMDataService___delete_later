using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMSDataService.DomainObjects.Models
{
    public class AccountType
    {
        [Key]
        public int AccountTypeID { get; set; }
        public string AccountTypeCode { get; set; }
        public string AccountTypeDescription { get; set; }
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
