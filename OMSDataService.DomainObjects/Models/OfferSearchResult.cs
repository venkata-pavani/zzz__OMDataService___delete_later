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
        public string AccountID { get; set; }
        public string AccountName { get; set; }
        public int CommodityID { get; set; }
        public string CommodityName { get; set; }
        public string ContractNumber { get; set; }
        public string ContractDate { get; set; }
        public DateTime? ContractDateTime { get; set; }
        public string DeliveryStartDate { get; set; }
        public DateTime? DeliveryStart { get; set; }
        public string DeliveryEndDate { get; set; }
        public DateTime? DeliveryEnd { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Basis { get; set; }
        public decimal? FuturesPrice { get; set; }
        public decimal? CashPrice { get; set; }
        public string ContractTransactionType { get; set; }
        public string OfferStatusType { get; set; }
        public string MarketZone { get; set; }
        public string AdvisorName { get; set; }
    }
}
