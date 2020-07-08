using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMSDataService.DomainObjects.Models
{
    public class ContractGraphData
    {
        public string Label { get; set; }
        public decimal Value { get; set; }
        public string Color { get; set; }
    }

    public class ContractSearchResult
    {
        public string LocationName { get; set; }
        public string ContractTypeName { get; set; }
        public string AccountName { get; set; }
        public string CommodityName { get; set; }
        public string ContractNumber { get; set; }
        public string ContractDate { get; set; }
        public string DeliveryStartDate { get; set; }
        public string DeliveryEndDate { get; set; }
        public string Quantity { get; set; }
        public string ContractStatus { get; set; }
        public string PricingStatus { get; set; }
        public string DeliveryBasis { get; set; }
        public string OptionMonth { get; set; }
        public string Basis { get; set; }
        public string FuturesPrice { get; set; }
        public string CashPrice { get; set; }
        public string InternalNotes { get; set; }
        public string AdditionalContractNotes { get; set; }
        public bool Printed { get; set; }
        public string MarketZoneName { get; set; }
        public string AppliedQuantity { get; set; }
        public string RemainingQuantity { get; set; }
        public string SettledQuantity { get; set; }
        public string UnsettledQuantity { get; set; }
        public string AdvisorName { get; set; }

        [NotMapped]
        public List<ContractGraphData> AppliedRemainingGraphData { get; set; }

        [NotMapped]
        public List<ContractGraphData> SettledUnsettledGraphData { get; set; }
    }
}
