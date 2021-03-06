using OMSDataService.DomainObjects.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMSDataService.DataInterfaces
{
    public interface IBidsheetRepository
    {
        Task<List<Bidsheet>> GetBidsheets();

        Task<List<BidsheetSearchResult>> GetBidsheetsToRollOfferTo(int locationId, int commodityId, int marketZoneId, bool useRealTimeQuotes);

        Task<Bidsheet> GetBidsheet(int bidsheetId);

        Task<BidsheetSearchResult> GetBidsheetWithFutureValues(int bidsheetID, bool useRealTimeQuotes);

        Task<Bidsheet> GetNewBidsheet();

        void AddBidsheet(Bidsheet item);

        void UpdateBidsheet(Bidsheet item);

        Task<List<BidsheetSearchResult>> SearchBidsheets(int? locationId, int? commodityId, bool active, bool countHasOffers, bool countHasOffersByAccountOnly,
                                                         int? accountID, bool useRealTimeQuotes);

        Task<List<BidsheetSearchResult>> SearchBidsheetsArchive(int? locationId, int? commodityId, DateTime? archiveStartDate, DateTime? archiveEndDate);
    }
}
