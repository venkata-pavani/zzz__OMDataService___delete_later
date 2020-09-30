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
using Microsoft.AspNetCore.Authorization;

namespace OMSDataService.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private IContractRepository _repo;
        private IBidsheetRepository _bidsheetRepository;
        private readonly ILogger _logger;

        public ContractController(IContractRepository repo, IBidsheetRepository bidsheetRepository, ILogger logger)
        {
            _repo = repo;
            _bidsheetRepository = bidsheetRepository;
            _logger = logger;
        }

        [ActionName("GetOffers")]
        [HttpGet]
        public async Task<IActionResult> GetOffers(int accountId)
        {
            try
            {
                return Ok(await _repo.GetOffers(accountId));
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
                return Ok(await _repo.GetOffersOnContract(contractNumber));
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
                return Ok(await _repo.GetOffersOnBidsheet(bidsheetID, getOffersByAccountOnly, accountID));
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
                return Ok(await _repo.GetContracts(accountExternalRef));
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
                return Ok(await _repo.GetContract(contractId));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetContract failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetNewContract")]
        [HttpGet]
        public async Task<IActionResult> GetNewContract(bool isSalesContract, bool isOffer, int? bidsheetID, int? contractTypeID, int accountID, bool useRealTimeQuotes)
        {
            try
            {
                BidsheetSearchResult bidsheet = null;

                if (bidsheetID.HasValue)
                {
                    bidsheet = await _bidsheetRepository.GetBidsheetWithFutureValues(bidsheetID.Value, useRealTimeQuotes);
                }

                return Ok(await _repo.GetNewContract(isSalesContract, isOffer, bidsheet, contractTypeID, accountID));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetNewContract failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetNewOfferFromContract")]
        [HttpGet]
        public async Task<IActionResult> GetNewOfferFromContract(int contractNumber)
        {
            try
            {
                return Ok(await _repo.GetNewOfferFromContract(contractNumber));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetNewOfferFromContract failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetNewPricingFromContract")]
        [HttpGet]
        public async Task<IActionResult> GetNewPricingFromContract(int contractNumber)
        {
            try
            {
                return Ok(await _repo.GetNewPricingFromContract(contractNumber));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetNewPricingFromContract failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetNewOfferClone")]
        [HttpGet]
        public async Task<IActionResult> GetNewOfferClone(int contractID)
        {
            try
            {
                return Ok(await _repo.GetNewOfferClone(contractID));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetNewOfferClone failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetNewContractClone")]
        [HttpGet]
        public async Task<IActionResult> GetNewContractClone(int contractID)
        {
            try
            {
                return Ok(await _repo.GetNewContractClone(contractID));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetNewContractClone failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("AddContract")]
        [HttpPost]
        public async Task<IActionResult> AddContract([FromBody] JObject item)
        {
            try
            { 
                return Ok(await _repo.AddContract(item["contract"].ToObject<Contract>(), item["contractDetail"].ToObject<ContractDetail>()));
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

        [ActionName("DeleteContract")]
        [HttpPost]
        public IActionResult DeleteContract([FromBody] JObject item)
        {
            try
            {
                _repo.DeleteContract(item["contract"].ToObject<Contract>(), item["contractDetail"].ToObject<ContractDetail>());

                return Ok();
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "DeleteContract failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("ConvertOfferToContract")]
        [HttpPost]
        public IActionResult ConvertOfferToContract([FromBody] JObject item)
        {
            try
            {
                _repo.ConvertOfferToContract(item["contract"].ToObject<Contract>(), item["contractDetail"].ToObject<ContractDetail>());

                return Ok();
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "ConvertOfferToContract failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("RollOffer")]
        [HttpPost]
        public IActionResult RollOffer([FromBody] JObject item)
        {
            try
            {
                _repo.RollOffer(item["contract"].ToObject<Contract>(), item["contractDetail"].ToObject<ContractDetail>(), item["bidsheet"].ToObject<BidsheetSearchResult>());

                return Ok();
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "RollOffer failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("SearchContracts")]
        [HttpGet]
        public async Task<IActionResult> SearchContracts(string contractTransactionTypeExternalRef, string locationExternalRef, string commodityExternalRef, string commoditySymbol,
                                                         string customerName, string marketZoneExternalRef, string contractTypeExternalRef, string contractStatusTypeExternalRef,
                                                         string advisorExternalRef, DateTime? contractStartDate, DateTime? contractEndDate, DateTime? deliveryBeginStartDate,
                                                         DateTime? deliveryBeginEndDate, DateTime? deliveryEndStartDate, DateTime? deliveryEndEndDate,
                                                         string contractPricingStatusTypeExternalRef)
        {
            try
            {
                return Ok(await _repo.SearchContracts(contractTransactionTypeExternalRef, locationExternalRef, commodityExternalRef, commoditySymbol, customerName,
                                                      marketZoneExternalRef, contractTypeExternalRef, contractStatusTypeExternalRef, advisorExternalRef,
                                                      contractStartDate, contractEndDate, deliveryBeginStartDate, deliveryBeginEndDate, deliveryEndStartDate, deliveryEndEndDate,
                                                      contractPricingStatusTypeExternalRef));
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
                return Ok(await _repo.GetContractPricings(contractNumber));
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
                return Ok(await _repo.GetContractAmendments(contractNumber));
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
                return Ok(await _repo.SearchOffers(contractTransactionTypeID, locationID, commodityID, commoditySymbol, customerName, contractTypeID, offerStatusTypeID, marketZoneID,
                                                   advisorID, offerStartDate, offerEndDate, deliveryBeginStartDate, deliveryBeginEndDate, deliveryEndStartDate, deliveryEndEndDate));
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
                return Ok(await _repo.GetOffersAndContracts(accountId));
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
                return Ok(await _repo.SearchOffersAndContracts(contractTransactionTypeID, locationID, commodityID, commoditySymbol, customerName, contractTypeID, marketZoneID,
                                                               advisorID, createdStartDate, createdEndDate, deliveryBeginStartDate, deliveryBeginEndDate, deliveryEndStartDate,
                                                               deliveryEndEndDate));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "SearchOffersAndContracts failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("SearchPositionManagerOffers")]
        [HttpGet]
        public async Task<IActionResult> SearchPositionManagerOffers(int? commodityID, string commoditySymbol, string customerName, int? marketZoneID, int? advisorID)
        {
            try
            {
                return Ok(await _repo.SearchPositionManagerOffers(commodityID, commoditySymbol, customerName, marketZoneID, advisorID));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "SearchPositionManagerOffers failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }
    }
}
