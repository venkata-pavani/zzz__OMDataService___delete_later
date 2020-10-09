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
    public class TypeController : ControllerBase
    {
        private ITypeRepository _repo;
        private readonly ILogger _logger;

        public TypeController(ITypeRepository repo, ILogger logger)
        {
             _repo = repo;
            _logger = logger;
        }

        [ActionName("GetNewLocation")]
        [HttpGet]
        public async Task<IActionResult> GetNewLocation()
        {
            try
            {
                return Ok(await _repo.GetNewLocation());
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetNewLocation failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetLocation")]
        [HttpGet]
        public async Task<IActionResult> GetLocation(int locationID)
        {
            try
            {
                return Ok(await _repo.GetLocation(locationID));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetLocation failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("AddLocation")]
        [HttpPost]
        public IActionResult AddLocation([FromBody] Location location)
        {
            try
            {
                _repo.AddLocation(location);

                return Ok();
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "AddLocation failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("UpdateLocation")]
        [HttpPost]
        public IActionResult UpdateLocation([FromBody] Location location)
        {
            try
            {
                _repo.UpdateLocation(location);

                return Ok();
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "UpdateLocation failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetLocations")]
        [HttpGet]
        public async Task<IActionResult> GetLocations(bool sortForDropDownList)
        {
            try
            {
                return Ok(await _repo.GetLocations(sortForDropDownList));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetLocations failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetNewCommodity")]
        [HttpGet]
        public async Task<IActionResult> GetNewCommodity()
        {
            try
            {
                return Ok(await _repo.GetNewCommodity());
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetNewCommodity failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetCommodity")]
        [HttpGet]
        public async Task<IActionResult> GetCommodity(int commodityID)
        {
            try
            {
                return Ok(await _repo.GetCommodity(commodityID));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetCommodity failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("AddCommodity")]
        [HttpPost]
        public IActionResult AddCommodity([FromBody] Commodity commodity)
        {
            try
            {
                _repo.AddCommodity(commodity);

                return Ok();
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "AddCommodity failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("UpdateCommodity")]
        [HttpPost]
        public IActionResult UpdateCommodity([FromBody] Commodity commodity)
        {
            try
            {
                _repo.UpdateCommodity(commodity);

                return Ok();
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "UpdateCommodity failed: {ex.message}");
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
                return Ok(await _repo.GetCommodities(sortForDropDownList));
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
                return Ok(await _repo.GetContractTransactionTypes(sortForDropDownList));
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
                return Ok(await _repo.GetContractStatusTypes(sortForDropDownList));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetContractStatusTypes failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetNewContractType")]
        [HttpGet]
        public async Task<IActionResult> GetNewContractType()
        {
            try
            {
                return Ok(await _repo.GetNewContractType());
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetNewContractType failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetContractType")]
        [HttpGet]
        public async Task<IActionResult> GetContractType(int contractTypeID)
        {
            try
            {
                return Ok(await _repo.GetContractType(contractTypeID));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetContractType failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("AddContractType")]
        [HttpPost]
        public IActionResult AddContractType([FromBody] ContractType contractType)
        {
            try
            {
                _repo.AddContractType(contractType);

                return Ok();
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "AddContractType failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("UpdateContractType")]
        [HttpPost]
        public IActionResult UpdateContractType([FromBody] ContractType contractType)
        {
            try
            {
                _repo.UpdateContractType(contractType);

                return Ok();
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "UpdateContractType failed: {ex.message}");
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
                return Ok(await _repo.GetContractTypes(sortForDropDownList));
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
                return Ok(await _repo.GetMonths(sortForDropDownList));
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
                return Ok(await _repo.GetOfferDurationTypes(sortForDropDownList));
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
                return Ok(await _repo.GetOfferPriceTypes(sortForDropDownList));
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
                return Ok(await _repo.GetOfferTypes(sortForDropDownList));
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
                return Ok(await _repo.GetUnitsOfMeasure(sortForDropDownList));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetUnitsOfMeasure failed: {ex.message}");
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
                return Ok(await _repo.GetAccountTypes(sortForDropDownList));
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
                return Ok(await _repo.GetAdvisors(sortForDropDownList));
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
                return Ok(await _repo.GetMarketZones(sortForDropDownList));
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
                return Ok(await _repo.GetContractPricingStatusTypes(sortForDropDownList));
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
                return Ok(await _repo.GetOfferStatusTypes(sortForDropDownList));
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
                return Ok(await _repo.GetContractExportStatusTypes(sortForDropDownList));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetContractExportStatusTypes failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetNewGridLayout")]
        [HttpGet]
        public async Task<IActionResult> GetNewGridLayout()
        {
            try
            {
                return Ok(await _repo.GetNewGridLayout());
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetNewGridLayout failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetGridLayout")]
        [HttpGet]
        public async Task<IActionResult> GetGridLayout(int gridLayoutID)
        {
            try
            {
                return Ok(await _repo.GetGridLayout(gridLayoutID));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetGridLayout failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("AddGridLayout")]
        [HttpPost]
        public IActionResult AddGridLayout([FromBody] GridLayout gridLayout)
        {
            try
            {
                _repo.AddGridLayout(gridLayout);

                return Ok(gridLayout);
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "AddGridLayout failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("UpdateGridLayout")]
        [HttpPost]
        public IActionResult UpdateGridLayout([FromBody] GridLayout gridLayout)
        {
            try
            {
                _repo.UpdateGridLayout(gridLayout);

                return Ok(gridLayout);
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "UpdateGridLayout failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetGridLayouts")]
        [HttpGet]
        public async Task<IActionResult> GetGridLayouts(string gridName)
        {
            try
            {
                return Ok(await _repo.GetGridLayouts(gridName));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetGridLayouts failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetDefaultGridLayout")]
        [HttpGet]
        public async Task<IActionResult> GetDefaultGridLayout(string gridName)
        {
            try
            {
                return Ok(await _repo.GetDefaultGridLayout(gridName));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetDefaultGridLayout failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("SetDefaultGridLayout")]
        [HttpGet]
        public IActionResult SetDefaultGridLayout(int gridLayoutID)
        {
            try
            {
                _repo.SetDefaultGridLayout(gridLayoutID);

                return Ok();
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "SetDefaultGridLayout failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetNoteActivityTypes")]
        [HttpGet]
        public async Task<IActionResult> GetNoteActivityTypes(bool sortForDropDownList)
        {
            try
            {
                return Ok(await _repo.GetNoteActivityTypes(sortForDropDownList));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetNoteActivityTypes failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetNotePriorityTypes")]
        [HttpGet]
        public async Task<IActionResult> GetNotePriorityTypes(bool sortForDropDownList)
        {
            try
            {
                return Ok(await _repo.GetNotePriorityTypes(sortForDropDownList));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetNotePriorityTypes failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetNoteStatusTypes")]
        [HttpGet]
        public async Task<IActionResult> GetNoteStatusTypes(bool sortForDropDownList)
        {
            try
            {
                return Ok(await _repo.GetNoteStatusTypes(sortForDropDownList));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetNoteStatusTypes failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }
    }
}
