using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMSDataService.DataInterfaces;
using OMSDataService.DomainObjects.Models;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Authorization;

namespace OMSDataService.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountRepository _repo;
        private readonly ILogger _logger;

        public AccountController(IAccountRepository repo, ILogger logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [ActionName("GetAccounts")]
        [HttpGet]
        public async Task<IActionResult> GetAccounts(bool sortForDropDownList)
        {
            try
            {
                return Ok(await _repo.GetAccounts(sortForDropDownList));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetAccounts failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetCustomer")]
        [HttpGet]
        public async Task<IActionResult> GetCustomer(string externalRef)
        {
            try
            {
                return Ok(await _repo.GetCustomer(externalRef));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetCustomer failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetAccount")]
        [HttpGet]
        public async Task<IActionResult> GetAccount(int accountID)
        {
            try
            {
                return Ok(await _repo.GetAccount(accountID));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetAccount failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("SearchAccounts")]
        [HttpGet]
        public async Task<IActionResult> SearchAccounts(string accountName, string externalRef)
        {
            try
            {
                return Ok(await _repo.SearchAccounts(accountName, externalRef));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "SearchAccounts failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetAccountNotes")]
        [HttpGet]
        public async Task<IActionResult> GetAccountNotes(int accountID)
        {
            try
            {
                return Ok(await _repo.GetAccountNotes(accountID));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetAccountNotes failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetNewAccountNote")]
        [HttpGet]
        public async Task<IActionResult> GetNewAccountNote(int accountID)
        {
            try
            {
                return Ok(await _repo.GetNewAccountNote(accountID));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetNewAccountNote failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetAccountNote")]
        [HttpGet]
        public async Task<IActionResult> GetAccountNote(int noteID)
        {
            try
            {
                return Ok(await _repo.GetAccountNote(noteID));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetAccountNote failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("AddAccountNote")]
        [HttpPost]
        public IActionResult AddAccountNote([FromBody] Note note)
        {
            try
            {
                _repo.AddAccountNote(note);

                return Ok();
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "AddAccountNote failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("UpdateAccountNote")]
        [HttpPost]
        public IActionResult UpdateAccountNote([FromBody] Note note)
        {
            try
            {
                _repo.UpdateAccountNote(note);

                return Ok();
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "UpdateAccountNote failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }
    }
}
