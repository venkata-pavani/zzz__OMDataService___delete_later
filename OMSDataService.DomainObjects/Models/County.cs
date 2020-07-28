using System;
namespace OMSDataService.DomainObjects.Models
{
    public class County
    {
        public int CountyID { get; set; }
        public string CountyName { get; set; }
        public string FIPS { get; set; }
        public string StatePostalCode { get; set; }
        public int? StateID { get; set; }
        public bool IsActive { get; set; }
        public DateTime AddDate { get; set; }
        public int AddUserID { get; set; }
        public DateTime ChgDate { get; set; }
        public int ChgUserID { get; set; }
        public byte[] CountyTimestamp { get; set; }
    }
}
