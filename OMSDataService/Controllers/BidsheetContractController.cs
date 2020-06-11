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

namespace OMSDataService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BidsheetController : ControllerBase
    {
        private IBidsheetRepository _repo;
        private readonly ILogger _logger;
        public BidsheetController(IBidsheetRepository repo, ILogger logger)
        {
             _repo = repo;
            _logger = logger;
        }

        [ActionName("GetBidsheets")]
        [HttpGet]
        public async Task<IActionResult> GetBidsheets()
        {
            try
            {
                var list = await _repo.GetBidsheets();
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetBidsheets failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("GetBidsheet")]
        [HttpGet]
        public async Task<IActionResult> GetBidsheet(int bidsheetId)
        {
            try
            {
                var list = await _repo.GetBidsheet(bidsheetId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "GetBidsheet failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        [ActionName("UpdateBidsheet")]
        [HttpPost]
        public IActionResult UpdateBidsheet([FromBody] Bidsheet item)
        {
            try
            {
                _repo.UpdateBidsheet(item);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "UpdateBidsheet failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }


        [ActionName("AddBidsheet")]
        [HttpPost]
        public IActionResult AddBidsheet([FromBody] Bidsheet item)
        {
            try
            {
                _repo.AddBidsheet(item);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "AddBidsheet failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }


    }
}
