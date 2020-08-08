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
using Newtonsoft.Json.Linq;

namespace OMSDataService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private IContractRepository _repo;
        private readonly ILogger _logger;
        public ContractController(IContractRepository repo, ILogger logger)
        {
             _repo = repo;
            _logger = logger;
        }

        [ActionName("GetOffers")]
        [HttpGet]
        public async Task<IActionResult> GetOffers(int accountId)
        {
            try
            {
                var list = await _repo.GetOffers(accountId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetOffers failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetOffersOnContract")]
        [HttpGet]
        public async Task<IActionResult> GetOffersOnContract(int contractNumber)
        {
            try
            {
                var list = await _repo.GetOffersOnContract(contractNumber);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetOffersOnContract failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetOffersOnBidsheet")]
        [HttpGet]
        public async Task<IActionResult> GetOffersOnBidsheet(int bidsheetID, bool getOffersByAccountOnly, int? accountID)
        {
            try
            {
                var list = await _repo.GetOffersOnBidsheet(bidsheetID, getOffersByAccountOnly, accountID);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetOffersOnBidsheet failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetContracts")]
        [HttpGet]
        public async Task<IActionResult> GetContracts(string accountExternalRef)
        {
            try
            {
                var list = await _repo.GetContracts(accountExternalRef);
                return Ok(list);
            }
            catch (Exception ex)
            { 
                _logger.Write(LogEventLevel.Error, ex, "GetContracts failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetContract")]
        [HttpGet]
        public async Task<IActionResult> GetContract(int contractId)
        {
            try
            {
                var list = await _repo.GetContract(contractId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetContract failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("AddContract")]
        [HttpPost]
        public IActionResult AddContract([FromBody] JObject item)
        {
            try
            { 
                _repo.AddContract(item["contract"].ToObject<Contract>(), item["contractDetail"].ToObject<ContractDetail>());

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "AddContract failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("UpdateContract")]
        [HttpPost]
        public IActionResult UpdateContract([FromBody] JObject item)
        {
            try
            {
                _repo.UpdateContract(item["contract"].ToObject<Contract>(), item["contractDetail"].ToObject<ContractDetail>());

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "UpdateContract failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("SearchContracts")]
        [HttpGet]
        public async Task<IActionResult> SearchContracts(string contractTransactionTypeExternalRef, string locationExternalRef, string commodityExternalRef, string commoditySymbol,
                                                         string customerName, string marketZoneExternalRef, string contractTypeExternalRef, string contractStatusTypeExternalRef,
                                                         string advisorExternalRef, DateTime? contractStartDate, DateTime? contractEndDate, DateTime? deliveryBeginStartDate,
                                                         DateTime? deliveryBeginEndDate, DateTime? deliveryEndStartDate, DateTime? deliveryEndEndDate)
        {
            try
            {
                var list = await _repo.SearchContracts(contractTransactionTypeExternalRef, locationExternalRef, commodityExternalRef, commoditySymbol, customerName,
                                                       marketZoneExternalRef, contractTypeExternalRef, contractStatusTypeExternalRef, advisorExternalRef,
                                                       contractStartDate, contractEndDate, deliveryBeginStartDate, deliveryBeginEndDate, deliveryEndStartDate, deliveryEndEndDate);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "SearchContracts failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetContractPricings")]
        [HttpGet]
        public async Task<IActionResult> GetContractPricings(int contractNumber)
        {
            try
            {
                var list = await _repo.GetContractPricings(contractNumber);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetContractPricings failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetContractAmendments")]
        [HttpGet]
        public async Task<IActionResult> GetContractAmendments(int contractNumber)
        {
            try
            {
                var list = await _repo.GetContractAmendments(contractNumber);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetContractAmendments failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("SearchOffers")]
        [HttpGet]
        public async Task<IActionResult> SearchOffers(int? contractTransactionTypeID, int? locationID, int? commodityID, string commoditySymbol, string customerName, int? contractTypeID,
                                                      int? offerStatusTypeID, int? marketZoneID, int? advisorID, DateTime? offerStartDate, DateTime? offerEndDate,
                                                      DateTime? deliveryBeginStartDate, DateTime? deliveryBeginEndDate, DateTime? deliveryEndStartDate, DateTime? deliveryEndEndDate)
        {
            try
            {
                var list = await _repo.SearchOffers(contractTransactionTypeID, locationID, commodityID, commoditySymbol, customerName, contractTypeID, offerStatusTypeID, marketZoneID,
                                                    advisorID, offerStartDate, offerEndDate, deliveryBeginStartDate, deliveryBeginEndDate, deliveryEndStartDate, deliveryEndEndDate);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "SearchOffers failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetOffersAndContracts")]
        [HttpGet]
        public async Task<IActionResult> GetOffersAndContracts(int accountId)
        {
            try
            {
                var list = await _repo.GetOffersAndContracts(accountId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetOffersAndContracts failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("SearchOffersAndContracts")]
        [HttpGet]
        public async Task<IActionResult> SearchOffersAndContracts(int? contractTransactionTypeID, int? locationID, int? commodityID, string commoditySymbol, string customerName,
                                                                  int? contractTypeID, int? marketZoneID, int? advisorID, DateTime? createdStartDate, DateTime? createdEndDate,
                                                                  DateTime? deliveryBeginStartDate, DateTime? deliveryBeginEndDate, DateTime? deliveryEndStartDate,
                                                                  DateTime? deliveryEndEndDate)
        {
            try
            {
                var list = await _repo.SearchOffersAndContracts(contractTransactionTypeID, locationID, commodityID, commoditySymbol, customerName, contractTypeID, marketZoneID,
                                                                advisorID, createdStartDate, createdEndDate, deliveryBeginStartDate, deliveryBeginEndDate, deliveryEndStartDate,
                                                                deliveryEndEndDate);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "SearchOffersAndContracts failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }
    }
}
