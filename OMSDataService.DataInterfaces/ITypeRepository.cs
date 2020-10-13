using OMSDataService.DomainObjects.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMSDataService.DataInterfaces
{
    public interface ITypeRepository
    {
        Task<Location> GetNewLocation();

        Task<Location> GetLocation(int locationID);

        void AddLocation(Location location);

        void UpdateLocation(Location location);

        Task<List<Location>> GetLocations(bool sortForDropDownList);

        Task<Commodity> GetNewCommodity();

        Task<Commodity> GetCommodity(int commodityID);

        void AddCommodity(Commodity commodity);

        void UpdateCommodity(Commodity commodity);

        Task<List<Commodity>> GetCommodities(bool sortForDropDownList);

        Task<List<ContractTransactionType>> GetContractTransactionTypes(bool sortForDropDownList);

        Task<List<ContractStatusType>> GetContractStatusTypes(bool sortForDropDownList);

        Task<ContractType> GetNewContractType();

        Task<ContractType> GetContractType(int contractTypeID);

        void AddContractType(ContractType contractType);

        void UpdateContractType(ContractType contractType);

        Task<List<ContractType>> GetContractTypes(bool sortForDropDownList);
        
        Task<List<Month>> GetMonths(bool sortForDropDownList);

        Task<List<OfferDurationType>> GetOfferDurationTypes(bool sortForDropDownList);

        Task<List<OfferPriceType>> GetOfferPriceTypes(bool sortForDropDownList);

        Task<List<OfferType>> GetOfferTypes(bool sortForDropDownList);

        Task<List<UnitOfMeasure>> GetUnitsOfMeasure(bool sortForDropDownList);

        Task<List<AccountType>> GetAccountTypes(bool sortForDropDownList);

        Task<List<Advisor>> GetAdvisors(bool sortForDropDownList);

        Task<List<MarketZone>> GetMarketZones(bool sortForDropDownList);

        Task<List<ContractPricingStatusType>> GetContractPricingStatusTypes(bool sortForDropDownList);

        Task<List<OfferStatusType>> GetOfferStatusTypes(bool sortForDropDownList);

        Task<List<ContractExportStatusType>> GetContractExportStatusTypes(bool sortForDropDownList);

        Task<GridLayout> GetNewGridLayout();

        Task<GridLayout> GetGridLayout(int gridLayoutID);

        void AddGridLayout(GridLayout gridLayout);

        void UpdateGridLayout(GridLayout gridLayout);

        Task<List<GridLayout>> GetGridLayouts(string gridName);

        Task<GridLayout> GetDefaultGridLayout(string gridName);

        void SetDefaultGridLayout(int gridLayoutID);

        Task<List<NotesActivityType>> GetNoteActivityTypes(bool sortForDropDownList);

        Task<List<NotesPriorityType>> GetNotePriorityTypes(bool sortForDropDownList);

        Task<List<NotesStatusType>> GetNoteStatusTypes(bool sortForDropDownList);
    }
}
