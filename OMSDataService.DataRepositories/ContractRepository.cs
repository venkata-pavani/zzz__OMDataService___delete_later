using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OMSDataService.DataInterfaces;
using OMSDataService.DomainObjects.Models;
using OMSDataService.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMSDataService.DataRepositories
{
    public class ContractRepository : IContractRepository
    { 

        private readonly IMapper _mapper;
        private ApiContext _context;

        public ContractRepository(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OfferSearchResult>> GetOffers(int accountId)
        {
            return await (from c in _context.Contracts
                          join cd in _context.ContractDetails on c.ContractID equals cd.ContractID
                          join a in _context.Accounts on cd.AccountID equals a.AccountID
                          join cmd in _context.Commodities on cd.CommodityID equals cmd.CommodityID
                          join ct in _context.ContractTypes on cd.ContractTypeID equals ct.ContractTypeID
                          join l in _context.Locations on cd.LocationID equals l.LocationID
                          join ctt in _context.ContractTransactionTypes on c.ContractTransactionTypeID equals ctt.ContractTransactionTypeID
                          where cd.AccountID == accountId && cd.Offer.Value == true
                          orderby c.ContractID
                          select new OfferSearchResult
                          {
                              AccountID = cd.AccountID.HasValue ? cd.AccountID.Value : 0,
                              AccountName = a.AccountName,
                              Basis = cd.OfferBasis,
                              CashPrice = cd.OfferCashPrice,
                              CommodityID = cd.CommodityID,
                              CommodityName = cmd.CommodityName,
                              ContractDate = cd.ContractDetailOfferDate.HasValue ? cd.ContractDetailOfferDate.Value.ToString("MM/dd/yyyy") : "",
                              ContractDateTime = cd.ContractDetailOfferDate,
                              ContractID = c.ContractID,
                              ContractNumber = c.ContractNumber,
                              ContractTypeID = cd.ContractTypeID,
                              ContractTypeName = ct.ContractTypeCode,
                              DeliveryEndDate = cd.DeliveryEndDate.HasValue ? cd.DeliveryEndDate.Value.ToString("MM/dd/yyyy") : "",
                              DeliveryEnd = cd.DeliveryEndDate,
                              DeliveryStartDate = cd.DeliveryStartDate.HasValue ? cd.DeliveryStartDate.Value.ToString("MM/dd/yyyy") : "",
                              DeliveryStart = cd.DeliveryStartDate,
                              FuturesPrice = cd.OfferFutures,
                              LocationID = cd.LocationID.HasValue ? cd.LocationID.Value : 0,
                              LocationName = l.LocationName,
                              Quantity = cd.Quantity,
                              ContractTransactionType = ctt.Description
                          }).ToListAsync();
        }

        [Obsolete]
        public async Task<List<ContractSearchResult>> GetContracts(string accountExternalRef)
        {
            var contracts = await _context.Query<ContractSearchResult>().FromSqlRaw("Execute dbo.GetContracts @AccountExternalRef = {0}", accountExternalRef).ToListAsync();

            foreach (var contract in contracts)
            {
                contract.AppliedRemainingGraphData = new List<ContractGraphData>();
                contract.SettledUnsettledGraphData = new List<ContractGraphData>();

                if (contract.Quantity <= 0)
                {
                    contract.AppliedRemainingGraphData.Add(new ContractGraphData()
                    {
                        Label = "Applied Quantity",
                        Value = 0M,
                        Color = "#075D3B"
                    });

                    contract.AppliedRemainingGraphData.Add(new ContractGraphData()
                    {
                        Label = "Remaining Quantity",
                        Value = 0M,
                        Color = "#FFE119"
                    });

                    contract.SettledUnsettledGraphData.Add(new ContractGraphData()
                    {
                        Label = "Settled Quantity",
                        Value = 0M,
                        Color = "#075D3B"
                    });

                    contract.SettledUnsettledGraphData.Add(new ContractGraphData()
                    {
                        Label = "Unsettled Quantity",
                        Value = 0M,
                        Color = "#FFE119"
                    });
                }

                else
                {
                    contract.AppliedRemainingGraphData.Add(new ContractGraphData()
                    {
                        Label = "Applied Quantity",
                        Value = Math.Round(contract.AppliedQuantity.Value / contract.Quantity, 4),
                        Color = "#075D3B"
                    });

                    contract.AppliedRemainingGraphData.Add(new ContractGraphData()
                    {
                        Label = "Remaining Quantity",
                        Value = Math.Round(contract.RemainingQuantity.Value / contract.Quantity, 4),
                        Color = "#FFE119"
                    });

                    contract.SettledUnsettledGraphData.Add(new ContractGraphData()
                    {
                        Label = "Settled Quantity",
                        Value = Math.Round(contract.SettledQuantity.Value / contract.Quantity, 4),
                        Color = "#075D3B"
                    });

                    contract.SettledUnsettledGraphData.Add(new ContractGraphData()
                    {
                        Label = "Unsettled Quantity",
                        Value = Math.Round(contract.UnsettledQuantity.Value / contract.Quantity, 4),
                        Color = "#FFE119"
                    });
                }

                contract.Pricing = await _context.Query<ContractPricing>().FromSqlRaw("Execute dbo.GetContractPricings @ContractNumber = {0}", contract.InternalContractNumber).ToListAsync();

                contract.Amendments = await _context.Query<ContractAmendment>().FromSqlRaw("Execute dbo.GetContractAmendments @ContractNumber = {0}", contract.InternalContractNumber).ToListAsync();
            }

            return contracts;
        }

        public async Task<Contract> GetContract(int contractId)
        {
            var item = await _context.Contracts
                  .FirstOrDefaultAsync(c => c.ContractID == contractId);  

            return item;
        }

        public void AddContract(Contract contract, ContractDetail contractDetail)
        {
            _context.Contracts.Add(contract);
            _context.SaveChanges();

            contractDetail.ContractID = contract.ContractID;

            _context.ContractDetails.Add(contractDetail);
            _context.SaveChanges();
        }

        public void UpdateContract(Contract item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        [Obsolete]
        public async Task<List<ContractSearchResult>> SearchContracts(string contractTransactionTypeExternalRef, string locationExternalRef, string commodityExternalRef, string customerName,
                                                                      DateTime? contractDate, DateTime? deliveryBeginDate, DateTime? deliveryEndDate)
        {
            return await _context.Query<ContractSearchResult>().FromSqlRaw("Execute dbo.SearchContracts " +
                                                                           "@ContractTransactionTypeExternalRef = {0}," +
                                                                           "@LocationExternalRef = {1}, " +
                                                                           "@CommodityExternalRef = {2}, " +
                                                                           "@CustomerName = {3}, " +
                                                                           "@ContractDate = {4}, " +
                                                                           "@DeliveryBeginDate = {5}, " +
                                                                           "@DeliveryEndDate = {6}",
                                                                           contractTransactionTypeExternalRef,
                                                                           locationExternalRef,
                                                                           commodityExternalRef,
                                                                           customerName,
                                                                           contractDate,
                                                                           deliveryBeginDate,
                                                                           deliveryEndDate)
                                                                           .ToListAsync();
        }

        public async Task<List<OfferSearchResult>> SearchOffers(int? contractTransactionTypeID, int? locationID, int? commodityID, string customerName,
                                                                DateTime? offerDate, DateTime? deliveryBeginDate, DateTime? deliveryEndDate)
        {
            return await (from c in _context.Contracts
                          join cd in _context.ContractDetails on c.ContractID equals cd.ContractID
                          join a in _context.Accounts on cd.AccountID equals a.AccountID
                          join cmd in _context.Commodities on cd.CommodityID equals cmd.CommodityID
                          join ct in _context.ContractTypes on cd.ContractTypeID equals ct.ContractTypeID
                          join l in _context.Locations on cd.LocationID equals l.LocationID
                          join ctt in _context.ContractTransactionTypes on c.ContractTransactionTypeID equals ctt.ContractTransactionTypeID
                          where cd.Offer.Value == true &&
                                (!contractTransactionTypeID.HasValue || c.ContractTransactionTypeID == contractTransactionTypeID.Value) &&
                                (!locationID.HasValue || cd.LocationID == locationID.Value) &&
                                (!commodityID.HasValue || cd.CommodityID == commodityID.Value) &&
                                (string.IsNullOrEmpty(customerName) || a.AccountName.Contains(customerName) || a.ExternalRef.Contains(customerName)) &&
                                (!offerDate.HasValue || cd.ContractDetailOfferDate.Value.Date == offerDate.Value.Date) &&
                                (!deliveryBeginDate.HasValue || cd.DeliveryStartDate.Value.Date == deliveryBeginDate.Value.Date) &&
                                (!deliveryEndDate.HasValue || cd.DeliveryEndDate.Value.Date == deliveryEndDate.Value.Date)
                          orderby c.ContractID
                          select new OfferSearchResult
                          {
                              AccountID = cd.AccountID.HasValue ? cd.AccountID.Value : 0,
                              AccountName = a.AccountName,
                              Basis = cd.OfferBasis,
                              CashPrice = cd.OfferCashPrice,
                              CommodityID = cd.CommodityID,
                              CommodityName = cmd.CommodityName,
                              ContractDate = cd.ContractDetailOfferDate.HasValue ? cd.ContractDetailOfferDate.Value.ToString("MM/dd/yyyy") : "",
                              ContractDateTime = cd.ContractDetailOfferDate,
                              ContractID = c.ContractID,
                              ContractNumber = c.ContractNumber,
                              ContractTypeID = cd.ContractTypeID,
                              ContractTypeName = ct.ContractTypeCode,
                              DeliveryEndDate = cd.DeliveryEndDate.HasValue ? cd.DeliveryEndDate.Value.ToString("MM/dd/yyyy") : "",
                              DeliveryEnd = cd.DeliveryEndDate,
                              DeliveryStartDate = cd.DeliveryStartDate.HasValue ? cd.DeliveryStartDate.Value.ToString("MM/dd/yyyy") : "",
                              DeliveryStart = cd.DeliveryStartDate,
                              FuturesPrice = cd.OfferFutures,
                              LocationID = cd.LocationID.HasValue ? cd.LocationID.Value : 0,
                              LocationName = l.LocationName,
                              Quantity = cd.Quantity,
                              ContractTransactionType = ctt.Description
                          }).Take(100).ToListAsync();
        }
    }
}
