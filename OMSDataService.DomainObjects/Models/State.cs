using System;
namespace OMSDataService.DomainObjects.Models
{
    public class State
    {
        public int StateID { get; set; }
        public string StateName { get; set; }
        public string StatePostalCode { get; set; }
        public string FIPS { get; set; }
        public bool IsActive { get; set; }
        public DateTime AddDate { get; set; }
        public int AddUserID { get; set; }
        public DateTime ChgDate { get; set; }
        public int ChgUserID { get; set; }
        public byte[] StateTimestamp { get; set; }
    }
}
