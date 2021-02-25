using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMSDataService.DomainObjects.Models
{
    public class ContractDetail
    {
        [Key]
        public int ContractDetailID { get; set; }
        public int ContractID { get; set; }
        public int? ContractTypeID { get; set; }
        public int? ContractStatusTypeID { get; set; }
        public int? CommodityID { get; set; }
        public int? AccountID { get; set; }
        public int? AdvisorID { get; set; }
        public DateTime? ContractDetailDate { get; set; }
        public int? OfferTypeID { get; set; }
        public int? OfferPriceTypeID { get; set; }
        public int? OfferDurationTypeID { get; set; }
        public DateTime? OfferGoodUntilDate { get; set; }
        public string Description { get; set; }
        public string ContractLetter { get; set; }
        public int? LocationID { get; set; }
        public int? BidsheetID { get; set; }
        public DateTime? DeliveryStartDate { get; set; }
        public DateTime? DeliveryEndDate { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? CashPrice { get; set; }
        public decimal? Futures { get; set; }
        public decimal? Basis { get; set; }
        public int? BatchNumber { get; set; }
        public decimal? RollBalance { get; set; }
        public string ExternalSystemRef { get; set; }
        public int? CropID { get; set; }
        public int? HarvestDelivery { get; set; }
        public int? HedgeMonthID { get; set; }
        public int? HedgeYear { get; set; }
        public int? HedgeTypeID { get; set; }
        public int? HedgeOrderID { get; set; }
        public string HedgeOrderRef { get; set; }
        public string RemarksToAccount { get; set; }
        public string RemarksToAdvisor { get; set; }
        public bool? FinalPrice { get; set; }
        public bool? Printed { get; set; }
        public int? ContractExportStatusTypeID { get; set; }
        public bool? IsArchived { get; set; }
        public DateTime AddDate { get; set; }
        public string AddUserID { get; set; }
        public DateTime ChgDate { get; set; }
        public string ChgUserID { get; set; }
        public DateTime? ContractDetailOfferDate { get; set; }
        public bool? Offer { get; set; }
        public int? OfferStatusTypeID { get; set; }
        public decimal? OfferCashPrice { get; set; }
        public decimal? OfferFutures { get; set; }
        public decimal? OfferBasis { get; set; }
        public int? MarketZoneID { get; set; }
        public decimal? FuturesOnAdd { get; set; }
        public DateTime? FuturesOnAddDateTime { get; set; }
        public decimal? FuturesOnInsert { get; set; }
        public DateTime? FuturesOnInsertDateTime { get; set; }
        public decimal? BasisOnAdd { get; set; }
        public DateTime? BasisOnAddDateTime { get; set; }
        public decimal? BasisOnInsert { get; set; } 
        public DateTime? BasisOnInsertDateTime { get; set; }
        public int? AdvisorReviewID { get; set; }
        public DateTime? AdvisorReviewDateTime { get; set; }
        public int? AdvisorManagerReviewID { get; set; }
        public DateTime? AdvisorManagerReviewDateTime { get; set; }
        public bool? WasOffer { get; set; }
        public bool Deleted { get; set; }
        public decimal? OfferHitBasis { get; set; }
        public decimal? OfferHitTickValue { get; set; }
        public DateTime? OfferHitDate { get; set; }
        public TimeSpan? OfferHitTime { get; set; }





    }
}
