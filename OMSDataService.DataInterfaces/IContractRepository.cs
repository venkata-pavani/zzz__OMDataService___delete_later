using OMSDataService.DomainObjects.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMSDataService.DataInterfaces
{
    public interface IContractRepository
    {
        Task<List<ContractSearchResult>> GetContracts(int accountId);
        Task<Contract> GetContract(int contractId);
        void AddContract(Contract item);
        void UpdateContract(Contract item);

    }
}
