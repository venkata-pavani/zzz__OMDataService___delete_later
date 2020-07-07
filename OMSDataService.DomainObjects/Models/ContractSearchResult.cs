﻿using System;
namespace OMSDataService.DomainObjects.Models
{
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
    }
}
