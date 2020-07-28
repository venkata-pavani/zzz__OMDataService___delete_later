using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMSDataService.DomainObjects.Models
{

    public class AccountSearchResult
    {
        public int AccountID { get; set; }
        public string AccountName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public string WebAddress { get; set; }
        public string ExternalRef { get; set; }
        public string ExternalRefName { get; set; }
    }
}
