using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMSDataService.DomainObjects.Models
{
    public class Advisor
    {
        [Key]
        public int AdvisorID { get; set; }
        public string AdvisorName { get; set; }
        public string Prefix { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? Zip { get; set; }
        public int? StateID { get; set; }
        public int? CountyID { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string MobilePhone { get; set; }
        public string Fax { get; set; }
        public string WebAddress { get; set; }
        public string Note { get; set; }
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
