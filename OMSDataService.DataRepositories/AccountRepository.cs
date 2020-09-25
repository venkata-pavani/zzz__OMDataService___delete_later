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
    public class AccountRepository : IAccountRepository
    {
        private readonly IMapper _mapper;
        private ApiContext _context;

        public AccountRepository(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Obsolete]
        public async Task<Customer> GetCustomer(string externalRef)
        {
            var customers = await _context.Query<Customer>().FromSqlRaw("Execute dbo.GetCustomer @ExternalRef = {0}", externalRef).ToListAsync();

            if (customers.Count > 0)
            {
                return customers[0];
            }

            else
            {
                return new Customer();
            }
        }

        public async Task<AccountSearchResult> GetAccount(int accountID)
        {
            return await (from a in _context.Accounts
                          join s in _context.States on a.StateID equals s.StateID
                          where a.AccountID == accountID
                          select new AccountSearchResult
                          {
                              AccountID = a.AccountID,
                              AccountName = a.AccountName,
                              Address1 = a.Address1,
                              Address2 = a.Address2,
                              City = a.City,
                              ExternalRef = a.ExternalRef,
                              ExternalRefName = a.ExternalRefName,
                              Fax = a.Fax,
                              Phone1 = a.Phone1,
                              Phone2 = a.Phone2,
                              State = s.StateName,
                              WebAddress = a.WebAddress,
                              Zip = a.Zip
                          }).SingleOrDefaultAsync();
        }

        public async Task<List<Account>> GetAccounts(bool sortForDropDownList)
        {
            if (sortForDropDownList)
            {
                return await _context.Accounts.OrderBy(a => a.SortOrder).ThenBy(a => a.AccountName).ToListAsync();
            }

            else
            {
                return await _context.Accounts.OrderBy(a => a.AccountName).ToListAsync();
            }
        }

        public async Task<List<AccountSearchResult>> SearchAccounts(string accountName, string externalRef)
        {
            var accountNameSearchString = !string.IsNullOrEmpty(accountName) ? accountName.Replace(" ", "") : "";
            var externalRefSearchString = !string.IsNullOrEmpty(externalRef) ? externalRef.Replace(" ", "") : "";

            var accounts = await (from a in _context.Accounts
                                  join s in _context.States on a.StateID equals s.StateID
                                  where (string.IsNullOrEmpty(accountName) || a.AccountName.Replace(" ", "").StartsWith(accountNameSearchString)) &&
                                        (string.IsNullOrEmpty(externalRef) || a.ExternalRef.Replace(" ", "").StartsWith(externalRefSearchString))
                                  select new AccountSearchResult
                                  {
                                      AccountID = a.AccountID,
                                      AccountName = a.AccountName,
                                      Address1 = a.Address1,
                                      Address2 = a.Address2,
                                      City = a.City,
                                      ExternalRef = a.ExternalRef,
                                      ExternalRefName = a.ExternalRefName,
                                      Fax = a.Fax,
                                      Phone1 = a.Phone1,
                                      Phone2 = a.Phone2,
                                      State = s.StateName,
                                      WebAddress = a.WebAddress,
                                      Zip = a.Zip
                                  }).OrderBy(a => a.AccountName).ToListAsync();

            if (accounts.Count == 0)
            {
                accounts = await (from a in _context.Accounts
                                  join s in _context.States on a.StateID equals s.StateID
                                  where (string.IsNullOrEmpty(accountName) || a.AccountName.Replace(" ", "").Contains(accountNameSearchString)) &&
                                        (string.IsNullOrEmpty(externalRef) || a.ExternalRef.Replace(" ", "").Contains(externalRefSearchString))
                                  select new AccountSearchResult
                                  {
                                      AccountID = a.AccountID,
                                      AccountName = a.AccountName,
                                      Address1 = a.Address1,
                                      Address2 = a.Address2,
                                      City = a.City,
                                      ExternalRef = a.ExternalRef,
                                      ExternalRefName = a.ExternalRefName,
                                      Fax = a.Fax,
                                      Phone1 = a.Phone1,
                                      Phone2 = a.Phone2,
                                      State = s.StateName,
                                      WebAddress = a.WebAddress,
                                      Zip = a.Zip
                                  }).OrderBy(a => a.AccountName).ToListAsync();
            }

            return accounts;
        }

        public async Task<List<NoteSearchResult>> GetAccountNotes(int accountID)
        {
            return await (from n in _context.Notes
                          join a in _context.NotesActivityTypes on n.NotesActivityTypeID equals a.NotesActivityTypeID
                          join p in _context.NotesPriorityTypes on n.NotesPriorityTypeID equals p.NotesPriorityTypeID
                          join s in _context.NotesStatusTypes on n.NotesStatusTypeID equals s.NotesStatusTypeID
                          join ad in _context.Advisors on n.AdvisorID equals ad.AdvisorID
                          where n.AccountID == accountID
                          orderby n.AddDate descending
                          select new NoteSearchResult()
                          {
                              AccountID = accountID,
                              AddDate = n.AddDate,
                              AddUserID = n.AddUserID,
                              AdvisorID = n.AdvisorID,
                              AdvisorName = ad.AdvisorName,
                              ChgDate = n.ChgDate,
                              ChgUserID = n.ChgUserID,
                              NoteID = n.NoteID,
                              NotesActivityType = a.NotesActivityTypeName,
                              NotesActivityTypeID = n.NotesActivityTypeID,
                              NotesPriority = p.NotesPriorityTypeName,
                              NotesPriorityTypeID = p.NotesPriorityTypeID,
                              NotesStatus = s.NotesStatusTypeName,
                              NotesStatusTypeID = n.NotesStatusTypeID,
                              NoteText = n.NoteText
                          }).ToListAsync();
        }

        public async Task<Note> GetNewAccountNote(int accountID)
        {
            return new Note()
            {
                NoteID = 0,
                AccountID = accountID,
                IsActive = true,
                NotesPriorityTypeID = 2,
                NotesStatusTypeID = 1
            };
        }

        public async Task<Note> GetAccountNote(int noteID)
        {
            return await _context.Notes.Where(n => n.NoteID == noteID).SingleOrDefaultAsync();
        }

        public void AddAccountNote(Note note)
        {
            note.AddDate = note.ChgDate = DateTime.Now;

            _context.Notes.Add(note);
            _context.SaveChanges();
        }

        public void UpdateAccountNote(Note note)
        {
            note.ChgDate = DateTime.Now;

            _context.Entry(note).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}