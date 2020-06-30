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
    public class ContractController : ControllerBase
    {
        private IContractRepository _repo;
        private readonly ILogger _logger;
        public ContractController(IContractRepository repo, ILogger logger)
        {
             _repo = repo;
            _logger = logger;
        }

        [ActionName("GetContracts")]
        [HttpGet]
        public async Task<IActionResult> GetContracts(int accountId)
        {
            try
            {
                var list = await _repo.GetContracts(accountId);
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


        [ActionName("AddContract")]
        [HttpPost]
        public IActionResult AddContract([FromBody] Contract item)
        {
            try
            {
                _repo.AddContract(item);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "AddContract failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }


    }
}
