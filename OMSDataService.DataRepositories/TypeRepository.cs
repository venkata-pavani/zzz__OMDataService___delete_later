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
    public class TypeRepository : ITypeRepository
    {
        private readonly IMapper _mapper;
        private ApiContext _context;

        public TypeRepository(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Location> GetNewLocation()
        {
            return new Location()
            {
                LocationID = 0,
                SortOrder = 0,
                IsActive = true
            };
        }

        public async Task<Location> GetLocation(int locationID)
        {
            return await _context.Locations.Where(l => l.LocationID == locationID).SingleOrDefaultAsync();
        }

        public void AddLocation(Location location)
        {
            location.AddDate = location.ChgDate = DateTime.Now;

            _context.Locations.Add(location);
            _context.SaveChanges();
        }

        public void UpdateLocation(Location location)
        {
            location.ChgDate = DateTime.Now;

            _context.Entry(location).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<List<Location>> GetLocations(bool sortForDropDownList)
        {
            if (sortForDropDownList)
            {
                return await _context.Locations.OrderBy(l => l.SortOrder).ThenBy(l => l.LocationName).ToListAsync();
            }

            else
            {
                return await _context.Locations.OrderBy(l => l.LocationName).ToListAsync();
            }
        }

        public async Task<Commodity> GetNewCommodity()
        { 
            return new Commodity()
            {
                CommodityID = 0,
                SortOrder = 0,
                IsActive = true
            };
        }

        public async Task<Commodity> GetCommodity(int commodityID)
        {
            return await _context.Commodities.Where(c => c.CommodityID == commodityID).SingleOrDefaultAsync();
        }

        public void AddCommodity(Commodity commodity)
        {
            commodity.AddDate = commodity.ChgDate = DateTime.Now;

            _context.Commodities.Add(commodity);
            _context.SaveChanges();
        }

        public void UpdateCommodity(Commodity commodity)
        {
            commodity.ChgDate = DateTime.Now;

            _context.Entry(commodity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<List<Commodity>> GetCommodities(bool sortForDropDownList)
        {
            if (sortForDropDownList)
            {
                return await _context.Commodities.OrderBy(c => c.SortOrder).ThenBy(c => c.CommodityName).ToListAsync();
            }

            else
            {
                return await _context.Commodities.OrderBy(c => c.CommodityName).ToListAsync();
            }
        }

        public async Task<List<ContractTransactionType>> GetContractTransactionTypes(bool sortForDropDownList)
        {
            if (sortForDropDownList)
            {
                return await _context.ContractTransactionTypes.OrderBy(c => c.SortOrder).ThenBy(c => c.Description).ToListAsync();
            }

            else
            {
                return await _context.ContractTransactionTypes.OrderBy(c => c.Description).ToListAsync();
            }
        }

        public async Task<List<ContractStatusType>> GetContractStatusTypes(bool sortForDropDownList)
        {
            if (sortForDropDownList)
            {
                return await _context.ContractStatusTypes.OrderBy(c => c.SortOrder).ThenBy(c => c.Description).ToListAsync();
            }

            else
            {
                return await _context.ContractStatusTypes.OrderBy(c => c.Description).ToListAsync();
            }
        }

        public async Task<ContractType> GetNewContractType()
        {
            return new ContractType()
            {
                ContractTypeID = 0,
                SortOrder = 0,
                IsActive = true
            };
        }

        public async Task<ContractType> GetContractType(int contractTypeID)
        {
            return await _context.ContractTypes.Where(c => c.ContractTypeID == contractTypeID).SingleOrDefaultAsync();
        }

        public void AddContractType(ContractType contractType)
        {
            contractType.AddDate = contractType.ChgDate = DateTime.Now;

            _context.ContractTypes.Add(contractType);
            _context.SaveChanges();
        }

        public void UpdateContractType(ContractType contractType)
        {
            contractType.ChgDate = DateTime.Now;

            _context.Entry(contractType).State = EntityState.Modified;
            _context.SaveChanges();
        }


        public async Task<List<ContractType>> GetContractTypes(bool sortForDropDownList)
        {
            if (sortForDropDownList)
            {
                return await _context.ContractTypes.OrderBy(c => c.SortOrder).ThenBy(c => c.Description).ToListAsync();
            }

            else
            {
                return await _context.ContractTypes.OrderBy(c => c.Description).ToListAsync();
            }
        }       

        public async Task<List<Month>> GetMonths(bool sortForDropDownList)
        {
            if (sortForDropDownList)
            {
                return await _context.Months.OrderBy(m => m.SortOrder).ThenBy(m => m.MonthID).ToListAsync();
            }

            else
            {
                return await _context.Months.OrderBy(m => m.MonthID).ToListAsync();
            }
        }

        public async Task<List<OfferDurationType>> GetOfferDurationTypes(bool sortForDropDownList)
        {
            if (sortForDropDownList)
            {
                return await _context.OfferDurationTypes.OrderBy(o => o.SortOrder).ThenBy(o => o.Description).ToListAsync();
            }

            else
            {
                return await _context.OfferDurationTypes.OrderBy(o => o.Description).ToListAsync();
            }
        }

        public async Task<List<OfferPriceType>> GetOfferPriceTypes(bool sortForDropDownList)
        {
            if (sortForDropDownList)
            {
                return await _context.OfferPriceTypes.OrderBy(o => o.SortOrder).ThenBy(o => o.OfferPriceTypeDescription).ToListAsync();
            }

            else
            {
                return await _context.OfferPriceTypes.OrderBy(o => o.OfferPriceTypeDescription).ToListAsync();
            }
        }


        public async Task<List<OfferType>> GetOfferTypes(bool sortForDropDownList)
        {
            if (sortForDropDownList)
            {
                return await _context.OfferTypes.OrderBy(o => o.SortOrder).ThenBy(o => o.OfferTypeDescription).ToListAsync();
            }

            else
            {
                return await _context.OfferTypes.OrderBy(o => o.OfferTypeDescription).ToListAsync();
            }
        }

        public async Task<List<UnitOfMeasure>> GetUnitsOfMeasure(bool sortForDropDownList)
        {
            if (sortForDropDownList)
            {
                return await _context.UnitsOfMeasure.OrderBy(u => u.SortOrder).ThenBy(u => u.UOM).ToListAsync();
            }

            else
            {
                return await _context.UnitsOfMeasure.OrderBy(u => u.UOM).ToListAsync();
            }
        }

        public async Task<List<AccountType>> GetAccountTypes(bool sortForDropDownList)
        {
            if (sortForDropDownList)
            {
                return await _context.AccountTypes.OrderBy(a => a.SortOrder).ThenBy(a => a.AccountTypeDescription).ToListAsync();
            }

            else
            {
                return await _context.AccountTypes.OrderBy(a => a.AccountTypeDescription).ToListAsync();
            }
        }

        public async Task<List<Advisor>> GetAdvisors(bool sortForDropDownList)
        {
            if (sortForDropDownList)
            {
                return await _context.Advisors.OrderBy(a => a.SortOrder).ThenBy(a => a.AdvisorName).ToListAsync();
            }

            else
            {
                return await _context.Advisors.OrderBy(a => a.AdvisorName).ToListAsync();
            }
        }

        public async Task<List<MarketZone>> GetMarketZones(bool sortForDropDownList)
        {
            if (sortForDropDownList)
            {
                return await _context.MarketZones.OrderBy(m => m.SortOrder).ThenBy(m => m.MarketZoneName).ToListAsync();
            }

            else
            {
                return await _context.MarketZones.OrderBy(m => m.MarketZoneName).ToListAsync();
            }
        }

        public async Task<List<ContractPricingStatusType>> GetContractPricingStatusTypes(bool sortForDropDownList)
        {
            if (sortForDropDownList)
            {
                return await _context.ContractPricingStatusTypes.OrderBy(c => c.SortOrder).ThenBy(c => c.Description).ToListAsync();
            }

            else
            {
                return await _context.ContractPricingStatusTypes.OrderBy(c => c.Description).ToListAsync();
            }
        }

        public async Task<List<OfferStatusType>> GetOfferStatusTypes(bool sortForDropDownList)
        {
            if (sortForDropDownList)
            {
                return await _context.OfferStatusTypes.OrderBy(o => o.SortOrder).ThenBy(o => o.OfferStatusTypeDescription).ToListAsync();
            }

            else
            {
                return await _context.OfferStatusTypes.OrderBy(o => o.OfferStatusTypeDescription).ToListAsync();
            }
        }

        public async Task<List<ContractExportStatusType>> GetContractExportStatusTypes(bool sortForDropDownList)
        {
            if (sortForDropDownList)
            {
                return await _context.ContractExportStatusTypes.OrderBy(o => o.SortOrder).ThenBy(o => o.ContractExportStatusTypeName).ToListAsync();
            }

            else
            {
                return await _context.ContractExportStatusTypes.OrderBy(o => o.ContractExportStatusTypeName).ToListAsync();
            }
        }

        public async Task<GridLayout> GetNewGridLayout()
        {
            return new GridLayout()
            {
                GridLayoutID = 0
            };
        }

        public async Task<GridLayout> GetGridLayout(int gridLayoutID)
        {
            return await _context.GridLayouts.Where(g => g.GridLayoutID == gridLayoutID).SingleOrDefaultAsync();
        }

        public void AddGridLayout(GridLayout gridLayout)
        {
            gridLayout.AddDate = gridLayout.ChgDate = DateTime.Now;

            _context.GridLayouts.Add(gridLayout);
            _context.SaveChanges();
        }

        public void UpdateGridLayout(GridLayout gridLayout)
        {
            gridLayout.ChgDate = DateTime.Now;

            _context.Entry(gridLayout).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<List<GridLayout>> GetGridLayouts(string gridName)
        {
            return await _context.GridLayouts.Where(g => g.GridName == gridName).OrderBy(g => g.LayoutName).ToListAsync();
        }

        public async Task<GridLayout> GetDefaultGridLayout(string gridName)
        {
            return await _context.GridLayouts.Where(g => g.GridName == gridName && g.IsDefaultLayout).SingleOrDefaultAsync();
        }

        public void SetDefaultGridLayout(int gridLayoutID)
        {
            var layout = _context.GridLayouts.Where(g => g.GridLayoutID == gridLayoutID).SingleOrDefault();

            if (layout != null)
            {
                var layouts = _context.GridLayouts.Where(g => g.GridName == layout.GridName).ToList();

                foreach (var l in layouts)
                {
                    l.IsDefaultLayout = false;

                    _context.Entry(l).State = EntityState.Modified;
                }

                layout.IsDefaultLayout = true;

                _context.SaveChanges();
            }
        }

        public async Task<List<NotesActivityType>> GetNoteActivityTypes(bool sortForDropDownList)
        {
            if (sortForDropDownList)
            {
                return await _context.NotesActivityTypes.OrderBy(n => n.SortOrder).ThenBy(n => n.NotesActivityTypeName).ToListAsync();
            }

            else
            {
                return await _context.NotesActivityTypes.OrderBy(n => n.NotesActivityTypeName).ToListAsync();
            }
        }

        public async Task<List<NotesPriorityType>> GetNotePriorityTypes(bool sortForDropDownList)
        {
            if (sortForDropDownList)
            {
                return await _context.NotesPriorityTypes.OrderBy(n => n.SortOrder).ThenBy(n => n.NotesPriorityTypeName).ToListAsync();
            }

            else
            {
                return await _context.NotesPriorityTypes.OrderBy(n => n.NotesPriorityTypeName).ToListAsync();
            }
        }

        public async Task<List<NotesStatusType>> GetNoteStatusTypes(bool sortForDropDownList)
        {
            if (sortForDropDownList)
            {
                return await _context.NotesStatusTypes.OrderBy(n => n.SortOrder).ThenBy(n => n.NotesStatusTypeName).ToListAsync();
            }

            else
            {
                return await _context.NotesStatusTypes.OrderBy(n => n.NotesStatusTypeName).ToListAsync();
            }
        }
    }
}
