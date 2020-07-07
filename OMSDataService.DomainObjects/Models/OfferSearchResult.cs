using System;
namespace OMSDataService.DomainObjects.Models
{
    public class OfferSearchResult
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
        public string Basis { get; set; }
        public string FuturesPrice { get; set; }
        public string CashPrice { get; set; }
    }
}
