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

    public class ContractPricing
    {
        public string Quantity { get; set; }
        public string Futures { get; set; }
        public string Basis { get; set; }
        public string OptionMonth { get; set; }
        public string CashPrice { get; set; }
        public string AddDate { get; set; }
        public string ChangeDate { get; set; }
        public string PriceDate { get; set; }
        public string AdvisorName { get; set; }
    }

    public class ContractAmendment
    {
        public string Note { get; set; }
        public string AmendmentDate { get; set; }
        public string PriceDate { get; set; }
        public string AdvisorName { get; set; }
        public bool Confirmed { get; set; }
        public bool Emailed { get; set; }
        public bool Printed { get; set; }
        public bool Signed { get; set; }
    }

    public class ContractSearchResult
    {
        public string LocationName { get; set; }
        public string ContractTypeName { get; set; }
        public int AccountID { get; set; }
        public string AccountName { get; set; }
        public string CommodityName { get; set; }
        public int InternalContractNumber { get; set; }
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
        public string ContractTransactionType { get; set; }

        [NotMapped]
        public List<ContractGraphData> AppliedRemainingGraphData { get; set; }

        [NotMapped]
        public List<ContractGraphData> SettledUnsettledGraphData { get; set; }

        [NotMapped]
        public List<ContractPricing> Pricing { get; set; }

        [NotMapped]
        public List<ContractAmendment> Amendments { get; set; }
    }
}
