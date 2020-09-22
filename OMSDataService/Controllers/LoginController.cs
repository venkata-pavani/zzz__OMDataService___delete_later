using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMSDataService.DataInterfaces;
using Serilog;
using Serilog.Events;
using OMSDataService.DomainObjects.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using OMSDataService.EF;
using Microsoft.EntityFrameworkCore;

namespace OMSDataService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ITypeRepository _repo;
        private readonly ILogger _logger;
        private readonly IAuthenticationService _authService;
        private readonly AuthOptions _authOptions;
        private ApiContext _context;

        public LoginController(ITypeRepository repo, ILogger logger, IAuthenticationService authService, IOptions<AuthOptions> authOptionsAccessor, ApiContext context)
        {
            _repo = repo;
            _logger = logger;
            _authService = authService;
            _authOptions = authOptionsAccessor.Value;
            _context = context;
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
                        var token = GenerateToken(username);

                        return Ok(new LdapUser
                        {
                            IsValidUser = true,
                            DisplayName = "Errol Adams",
                            Username = "errol_mac_user",
                            Token = new JwtSecurityTokenHandler().WriteToken(token),
                            TokenExpiration = token.ValidTo,
                            AdvisorID = 9,
                            RealTimeQuotes = false
                        });
                    }

                    else
                    {
                        return Ok(new LdapUser
                        {
                            IsValidUser = false,
                            DisplayName = "",
                            Username = "",
                            Token = null,
                            TokenExpiration = null,
                            AdvisorID = null,
                            RealTimeQuotes = false
                        });
                    }
                }

                else
                {
                    var user = _authService.Login(username, password);

                    if (user.IsValidUser)
                    {
                        var token = GenerateToken(username);

                        user.Token = new JwtSecurityTokenHandler().WriteToken(token);
                        user.TokenExpiration = token.ValidTo;

                        var dbUser = await _context.Users.Where(u => u.UserName == username).SingleOrDefaultAsync();

                        if (dbUser != null)
                        {
                            user.AdvisorID = dbUser.AdvisorID;
                            user.RealTimeQuotes = dbUser.RealTimeQuotes;
                        }

                        else
                        {
                            user.RealTimeQuotes = false;
                        }
                    }

                    else
                    {
                        user.Token = null;
                        user.TokenExpiration = null;
                    }

                    return Ok(user);
                }
            }

            catch (Exception ex)
            {
                _logger.Write(LogEventLevel.Error, ex, "ValidateUser failed: {ex.message}");
                var returnResult = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return BadRequest(returnResult);
            }
        }

        private JwtSecurityToken GenerateToken(string username)
        {
            var authClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            return new JwtSecurityToken(
                issuer: _authOptions.Issuer,
                audience: _authOptions.Audience,
                expires: DateTime.Now.AddMinutes(_authOptions.ExpiresInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.SecureKey)), SecurityAlgorithms.HmacSha256Signature)
            );
        }
    }
}
