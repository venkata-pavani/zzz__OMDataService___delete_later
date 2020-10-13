using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMSDataService.DomainObjects.Models
{
    public class Emails
    {
        [Key]
        public int Id { get; set; }
        public int EmailType { get; set; }
        public string EmailText { get; set; }
        public string Subject { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public DateTime AddDate { get; set; }
        public bool Sent { get; set; }
        public string PhoneNumber { get; set; }

         
    }
}
