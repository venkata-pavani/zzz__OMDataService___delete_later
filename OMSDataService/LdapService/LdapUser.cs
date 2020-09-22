using System;

namespace OMSDataService
{
    public class LdapUser
    {
        public bool IsValidUser { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpiration { get; set; }
        public int? AdvisorID { get; set; }
        public bool? RealTimeQuotes { get; set; }
    }
}
