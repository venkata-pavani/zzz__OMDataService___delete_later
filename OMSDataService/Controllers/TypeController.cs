using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMSDataService.DataInterfaces;
using Serilog;
using Serilog.Events;

namespace OMSDataService.Controllers
{
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

        [ActionName("GetCustomers")]
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var list = await _repo.GetCustomers();
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetCustomers failed: {ex.message}");
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
    }
}
