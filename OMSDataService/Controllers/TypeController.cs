using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMSDataService.DataInterfaces;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Authorization;

namespace OMSDataService.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private ITypeRepository _repo;
        private readonly ILogger _logger;

        public TypeController(ITypeRepository repo, ILogger logger)
        {
             _repo = repo;
            _logger = logger;
        }

        [ActionName("GetLocations")]
        [HttpGet]
        public async Task<IActionResult> GetLocations(bool sortForDropDownList)
        {
            try
            {
                var list = await _repo.GetLocations(sortForDropDownList);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetLocations failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetCommodities")]
        [HttpGet]
        public async Task<IActionResult> GetCommodities(bool sortForDropDownList)
        {
            try
            {
                var list = await _repo.GetCommodities(sortForDropDownList);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetCommodities failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetContractTransactionTypes")]
        [HttpGet]
        public async Task<IActionResult> GetContractTransactionTypes(bool sortForDropDownList)
        {
            try
            {
                var list = await _repo.GetContractTransactionTypes(sortForDropDownList);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetContractTransactionTypes failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetContractStatusTypes")]
        [HttpGet]
        public async Task<IActionResult> GetContractStatusTypes(bool sortForDropDownList)
        {
            try
            {
                var list = await _repo.GetContractStatusTypes(sortForDropDownList);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetContractStatusTypes failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetContractTypes")]
        [HttpGet]
        public async Task<IActionResult> GetContractTypes(bool sortForDropDownList)
        {
            try
            {
                var list = await _repo.GetContractTypes(sortForDropDownList);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetContractTypes failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }
                

        [ActionName("GetMonths")]
        [HttpGet]
        public async Task<IActionResult> GetMonths(bool sortForDropDownList)
        {
            try
            {
                var list = await _repo.GetMonths(sortForDropDownList);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetMonths failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetOfferDurationTypes")]
        [HttpGet]
        public async Task<IActionResult> GetOfferDurationTypes(bool sortForDropDownList)
        {
            try
            {
                var list = await _repo.GetOfferDurationTypes(sortForDropDownList);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetOfferDurationTypes failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetOfferPriceTypes")]
        [HttpGet]
        public async Task<IActionResult> GetOfferPriceTypes(bool sortForDropDownList)
        {
            try
            {
                var list = await _repo.GetOfferPriceTypes(sortForDropDownList);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetOfferPriceTypes failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetOfferTypes")]
        [HttpGet]
        public async Task<IActionResult> GetOfferTypes(bool sortForDropDownList)
        {
            try
            {
                var list = await _repo.GetOfferTypes(sortForDropDownList);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetOfferTypes failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetUnitsOfMeasure")]
        [HttpGet]
        public async Task<IActionResult> GetUnitsOfMeasure(bool sortForDropDownList)
        {
            try
            {
                var list = await _repo.GetUnitsOfMeasure(sortForDropDownList);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetUnitsOfMeasure failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetAccounts")]
        [HttpGet]
        public async Task<IActionResult> GetAccounts(bool sortForDropDownList)
        {
            try
            {
                var list = await _repo.GetAccounts(sortForDropDownList);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetAccounts failed: {ex.message}");
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
                var list = await _repo.SearchAccounts(accountName, externalRef);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "SearchAccounts failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetAccountTypes")]
        [HttpGet]
        public async Task<IActionResult> GetAccountTypes(bool sortForDropDownList)
        {
            try
            {
                var list = await _repo.GetAccountTypes(sortForDropDownList);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetAccountTypes failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetAdvisors")]
        [HttpGet]
        public async Task<IActionResult> GetAdvisors(bool sortForDropDownList)
        {
            try
            {
                var list = await _repo.GetAdvisors(sortForDropDownList);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetAdvisors failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetMarketZones")]
        [HttpGet]
        public async Task<IActionResult> GetMarketZones(bool sortForDropDownList)
        {
            try
            {
                var list = await _repo.GetMarketZones(sortForDropDownList);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetMarketZones failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetContractPricingStatusTypes")]
        [HttpGet]
        public async Task<IActionResult> GetContractPricingStatusTypes(bool sortForDropDownList)
        {
            try
            {
                var list = await _repo.GetContractPricingStatusTypes(sortForDropDownList);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetContractPricingStatusTypes failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetOfferStatusTypes")]
        [HttpGet]
        public async Task<IActionResult> GetOfferStatusTypes(bool sortForDropDownList)
        {
            try
            {
                var list = await _repo.GetOfferStatusTypes(sortForDropDownList);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetOfferStatusTypes failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetContractExportStatusTypes")]
        [HttpGet]
        public async Task<IActionResult> GetContractExportStatusTypes(bool sortForDropDownList)
        {
            try
            {
                var list = await _repo.GetContractExportStatusTypes(sortForDropDownList);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetContractExportStatusTypes failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }
    }
}
