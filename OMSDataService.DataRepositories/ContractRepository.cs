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

        public async Task<List<Contract>> GetContracts()
        {
            var list = await _context.Contracts
                .OrderByDescending(x => x.ContractID)
                    .ToListAsync();          

            return list;
        }

        public async Task<Contract> GetContract(int contractId)
        {
            var item = await _context.Contracts
                  .FirstOrDefaultAsync(c => c.ContractID == contractId);  

            return item;
        }

        public void AddContract(Contract item)
        {

            _context.Contracts.Add(item);
            _context.SaveChanges();
        }

        public void UpdateContract(Contract item)
        {
            //var existingItem = GetPricingItem(vm.Id);

            //_mapper.Map(vm, existingItem);

            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }


    }


}
