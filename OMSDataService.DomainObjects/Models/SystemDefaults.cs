using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMSDataService.DomainObjects.Models
{
    public class SystemDefaults
    {

        [Key]
        public int SystemDefaultID { get; set; }
        public string TwilioAccountSID { get; set; }
        public string TwilioAuthToken { get; set; }
        public Int64 TwilioPhoneNumber { get; set; }
    }
}
