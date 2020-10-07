using OMSDataService.DomainObjects.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMSDataService.DataInterfaces
{
    public interface IHelperRepository
    {

        Task<List<TickHistoryFutures>> GetTicksForOffers();
        Task<List<Emails>> SendOfferEmailItems();
    }
}
