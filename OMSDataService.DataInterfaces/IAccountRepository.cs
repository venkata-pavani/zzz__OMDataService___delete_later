using OMSDataService.DomainObjects.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMSDataService.DataInterfaces
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAccounts(bool sortForDropDownList);

        Task<List<AccountSearchResult>> SearchAccounts(string accountName, string externalRef);

        Task<Customer> GetCustomer(string externalRef);

        Task<AccountSearchResult> GetAccount(int accountID);

        Task<List<NoteSearchResult>> GetAccountNotes(int accountID);

        Task<Note> GetNewAccountNote(int accountID);

        Task<Note> GetAccountNote(int noteID);

        void AddAccountNote(Note note);

        void UpdateAccountNote(Note note);
    }
}
