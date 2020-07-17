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
        public IActionResult UpdateContract([FromBody] Contract item)
        {
            try
            {
                _repo.UpdateContract(item);

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
        public async Task<IActionResult> SearchContracts(string contractTransactionTypeExternalRef, string locationExternalRef, string commodityExternalRef, string customerName,
                                                         DateTime? contractDate, DateTime? deliveryBeginDate, DateTime? deliveryEndDate)
        {
            try
            {
                var list = await _repo.SearchContracts(contractTransactionTypeExternalRef, locationExternalRef, commodityExternalRef, customerName, contractDate,
                                                       deliveryBeginDate, deliveryEndDate);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "SearchContracts failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("SearchOffers")]
        [HttpGet]
        public async Task<IActionResult> SearchOffers(int? contractTransactionTypeID, int? locationID, int? commodityID, string customerName,
                                                      DateTime? offerDate, DateTime? deliveryBeginDate, DateTime? deliveryEndDate)
        {
            try
            {
                var list = await _repo.SearchOffers(contractTransactionTypeID, locationID, commodityID, customerName, offerDate,
                                                    deliveryBeginDate, deliveryEndDate);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "SearchOffers failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }
    }
}
