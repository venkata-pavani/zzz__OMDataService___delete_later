using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMSDataService.DomainObjects.Models
{
    public class ContractGraphData
    {
        public string Label { get; set; }
        public decimal? Value { get; set; }
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
        public string AccountID { get; set; }
        public string AccountName { get; set; }
        public string CommodityName { get; set; }
        public int? InternalContractNumber { get; set; }
        public string ContractNumber { get; set; }
        public string ContractDate { get; set; }
        public DateTime? ContractDateTime { get; set; }
        public string DeliveryStartDate { get; set; }
        public DateTime? DeliveryStart { get; set; }
        public string DeliveryEndDate { get; set; }
        public DateTime? DeliveryEnd { get; set; }
        public decimal? Quantity { get; set; }
        public string ContractStatus { get; set; }
        public string PricingStatus { get; set; }
        public string DeliveryBasis { get; set; }
        public string OptionMonth { get; set; }
        public decimal? Basis { get; set; }
        public decimal? FuturesPrice { get; set; }
        public decimal? CashPrice { get; set; }
        public string InternalNotes { get; set; }
        public string AdditionalContractNotes { get; set; }
        public bool Printed { get; set; }
        public string MarketZoneName { get; set; }
        public string AdvisorName { get; set; }
        public string ContractTransactionType { get; set; }
        public bool HasOffers { get; set; }
        public int? LocationID { get; set; }
        public int? OMSAccountID { get; set; }
        public int? ContractTypeID { get; set; }
        public int? CommodityID { get; set; }
        public int? ContractStatusTypeID { get; set; }
        public int? ContractPricingStatusTypeID { get; set; }
        public int? MarketZoneID { get; set; }
        public int? AdvisorID { get; set; }
        public int? ContractTransactionTypeID { get; set; }
        public decimal? AppliedQuantity { get; set; }
        public decimal? RemainingQuantity { get; set; }
        public decimal? SettledQuantity { get; set; }
        public decimal? UnsettledQuantity { get; set; }
        public decimal? ActualAppliedQuantity { get; set; }
        public decimal? AllocatedQuantity { get; set; }
        public decimal? AvailableQuantity { get; set; }
        public decimal? EstimatedAppliedQuantity { get; set; }
        public double? LoadoutQuantity { get; set; }
        public decimal? ScheduledQuantity { get; set; }
        public decimal? ScheduledLessLoadoutQuantity { get; set; }
        public decimal? ScheduledLoads { get; set; }
        public decimal? WashedCancelledQuantity { get; set; }
        public decimal? MzAdj { get; set; }

        [NotMapped]
        public List<ContractGraphData> AppliedRemainingGraphData { get; set; }

        [NotMapped]
        public List<ContractGraphData> SettledUnsettledGraphData { get; set; }

        [NotMapped]
        public List<ContractPricing> Pricing { get; set; }

        [NotMapped]
        public List<ContractAmendment> Amendments { get; set; }

        [NotMapped]
        public List<OfferSearchResult> Offers { get; set; }
    }
}
