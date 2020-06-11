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
    public class BidsheetRepository : IBidsheetRepository
    {
        private readonly IMapper _mapper;
        private ApiContext _context;

        public BidsheetRepository(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Bidsheet>> GetBidsheets()
        {
            var list = await _context.Bidsheets
                .OrderByDescending(x => x.BidsheetID)
                    .ToListAsync();          

            return list;
        }

        public async Task<Bidsheet> GetBidsheet(int bidsheetId)
        {
            var item = await _context.Bidsheets
                  .FirstOrDefaultAsync(c => c.BidsheetID == bidsheetId);  

            return item;
        }

        public void AddBidsheet(Bidsheet item)
        {

            _context.Bidsheets.Add(item);
            _context.SaveChanges();
        }

        public void UpdateBidsheet(Bidsheet item)
        {
            //var existingItem = GetPricingItem(vm.Id);

            //_mapper.Map(vm, existingItem);

            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }


    }


}
