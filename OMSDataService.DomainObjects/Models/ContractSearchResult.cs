using System;
namespace OMSDataService.DomainObjects.Models
{
    public class ContractSearchResult
    {
        public int ContractID { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public int ContractTypeID { get; set; }
        public string ContractTypeName { get; set; }
        public int AccountID { get; set; }
        public string AccountName { get; set; }
        public int CommodityID { get; set; }
        public string CommodityName { get; set; }
        public string ContractNumber { get; set; }
        public string ContractDate { get; set; }
        public string DeliveryStartDate { get; set; }
        public string DeliveryEndDate { get; set; }
        public string Quantity { get; set; }
        public bool HasOffers { get; set; }
        public bool Signed { get; set; }
        public bool Exported { get; set; }
        public bool Cancelled { get; set; }
        public bool Priced { get; set; }
    }
}
