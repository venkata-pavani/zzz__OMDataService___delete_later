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
    public class LoginController : ControllerBase
    {
        private ITypeRepository _repo;
        private readonly ILogger _logger;
        private readonly IAuthenticationService _authService;

        public LoginController(ITypeRepository repo, ILogger logger, IAuthenticationService authService)
        {
            _repo = repo;
            _logger = logger;
            _authService = authService;
        }

        [ActionName("ValidateUser")]
        [HttpGet]
        public async Task<IActionResult> ValidateUser(string username, string password)
        {
            try
            {
                // HACK FOR Visual Studio for Mac DEVELOPMENT
                if (username == "errol_mac_user")
                {
                    if (password == "hack4MAC!!!")
                    {
                        return Ok(new LdapUser
                        {
                            IsValidUser = true,
                            DisplayName = "Errol Adams",
                            Username = "errol_mac_user"
                        });
                    }

                    else
                    {
                        return Ok(new LdapUser
                        {
                            IsValidUser = false,
                            DisplayName = "",
                            Username = ""
                        });
                    }
                }

                return Ok(_authService.Login(username, password));
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "ValidateUser failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }
    }
}
