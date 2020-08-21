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
        public async Task<IActionResult> GetLocations()
        {
            try
            {
                var list = await _repo.GetLocations();
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
        public async Task<IActionResult> GetCommodities()
        {
            try
            {
                var list = await _repo.GetCommodities();
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
        public async Task<IActionResult> GetContractTransactionTypes()
        {
            try
            {
                var list = await _repo.GetContractTransactionTypes();
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
        public async Task<IActionResult> GetContractStatusTypes()
        {
            try
            {
                var list = await _repo.GetContractStatusTypes();
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
        public async Task<IActionResult> GetContractTypes()
        {
            try
            {
                var list = await _repo.GetContractTypes();
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
        public async Task<IActionResult> GetMonths()
        {
            try
            {
                var list = await _repo.GetMonths();
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
        public async Task<IActionResult> GetOfferDurationTypes()
        {
            try
            {
                var list = await _repo.GetOfferDurationTypes();
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
        public async Task<IActionResult> GetOfferPriceTypes()
        {
            try
            {
                var list = await _repo.GetOfferPriceTypes();
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
        public async Task<IActionResult> GetOfferTypes()
        {
            try
            {
                var list = await _repo.GetOfferTypes();
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
        public async Task<IActionResult> GetUnitsOfMeasure()
        {
            try
            {
                var list = await _repo.GetUnitsOfMeasure();
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
        public async Task<IActionResult> GetAccounts()
        {
            try
            {
                var list = await _repo.GetAccounts();
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
        public async Task<IActionResult> GetAccountTypes()
        {
            try
            {
                var list = await _repo.GetAccountTypes();
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
        public async Task<IActionResult> GetAdvisors()
        {
            try
            {
                var list = await _repo.GetAdvisors();
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
        public async Task<IActionResult> GetMarketZones()
        {
            try
            {
                var list = await _repo.GetMarketZones();
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
        public async Task<IActionResult> GetContractPricingStatusTypes()
        {
            try
            {
                var list = await _repo.GetContractPricingStatusTypes();
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
        public async Task<IActionResult> GetOfferStatusTypes()
        {
            try
            {
                var list = await _repo.GetOfferStatusTypes();
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
        public async Task<IActionResult> GetContractExportStatusTypes()
        {
            try
            {
                var list = await _repo.GetContractExportStatusTypes();
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
