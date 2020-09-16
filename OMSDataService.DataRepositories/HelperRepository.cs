using AutoMapper;
using OMSDataService.DomainObjects.Models;
using OMSDataService.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OMSDataService.DataRepositories
{
    public class HelperRepository
    {
        private readonly IMapper _mapper;
        private ApiContext _context;

        public HelperRepository(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        } 

        //public async Task<List<EmailItems>> SendItemsToQueue(int accountId)
        //{
        //    var emailItemResponse = new EmailItems();
        //    //await emailItemResponse;
        //    //return await (from c in _context.Contracts
        //    //              join cd in _context.ContractDetails on c.ContractID equals cd.ContractID
        //    //              join a in _context.Accounts on cd.AccountID equals a.AccountID
        //    //              join cmd in _context.Commodities on cd.CommodityID equals cmd.CommodityID
        //    //              join ct in _context.ContractTypes on cd.ContractTypeID equals ct.ContractTypeID
        //    //              join l in _context.Locations on cd.LocationID equals l.LocationID
        //    //              join ctt in _context.ContractTransactionTypes on c.ContractTransactionTypeID equals ctt.ContractTransactionTypeID
        //    //              join ost in _context.OfferStatusTypes on cd.OfferStatusTypeID equals ost.OfferStatusTypeID
        //    //              join m in _context.MarketZones on cd.MarketZoneID equals m.MarketZoneID
        //    //              join ad in _context.Advisors on cd.AdvisorID equals ad.AdvisorID
        //    //              join mo in _context.Months on cd.HedgeMonthID equals mo.MonthID
        //    //              where cd.AccountID == accountId && cd.Offer.Value == true && !c.Deleted
        //    //              orderby c.ContractID
        //    //              select new OfferSearchResult
        //    //              {
        //    //                  AccountID = a.ExternalRef,
        //    //                  AccountName = a.AccountName,
        //    //                  Basis = cd.OfferBasis,
        //    //                  CashPrice = cd.OfferCashPrice,
        //    //                  CommodityID = cd.CommodityID,
        //    //                  CommodityName = cmd.CommodityName,
        //    //                  ContractDate = cd.ContractDetailOfferDate.HasValue ? cd.ContractDetailOfferDate.Value.ToString("MM/dd/yyyy") : "",
        //    //                  ContractDateTime = cd.ContractDetailOfferDate,
        //    //                  ContractID = c.ContractID,
        //    //                  ContractNumber = c.ContractNumber,
        //    //                  ContractTypeID = cd.ContractTypeID,
        //    //                  ContractTypeName = ct.ContractTypeCode,
        //    //                  DeliveryEndDate = cd.DeliveryEndDate.HasValue ? cd.DeliveryEndDate.Value.ToString("MM/dd/yyyy") : "",
        //    //                  DeliveryEnd = cd.DeliveryEndDate,
        //    //                  DeliveryStartDate = cd.DeliveryStartDate.HasValue ? cd.DeliveryStartDate.Value.ToString("MM/dd/yyyy") : "",
        //    //                  DeliveryStart = cd.DeliveryStartDate,
        //    //                  FuturesPrice = cd.OfferFutures,
        //    //                  LocationID = cd.LocationID.HasValue ? cd.LocationID.Value : 0,
        //    //                  LocationName = l.LocationName,
        //    //                  Quantity = cd.Quantity,
        //    //                  ContractTransactionType = ctt.Description,
        //    //                  OfferStatusType = ost.OfferStatusTypeDescription,
        //    //                  MarketZone = m.Description,
        //    //                  AdvisorName = ad.AdvisorName,
        //    //                  CommoditySymbol = cmd.Symbol + mo.MonthCode + cd.HedgeYear.ToString().Substring(3, 1),
        //    //                  StatusColor = ost.StatusColor
        //    //              }).Take(1000).ToListAsync();
        //}
    }
}
