using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AzureServiceBus.Identity.API.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AzureServiceBus.Identity.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="configuration"></param>
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
        }

        /// <summary>
        /// Return User details with Token when authenticated successfully
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(IReadOnlyList<ApplicationUser>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LoginAsync(string username, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    return BadRequest("UserName or Password is blank!");
                var user = await _userManager.FindByEmailAsync(username);
                if (user != null)
                {
                    var checkPassword = await _userManager.CheckPasswordAsync(user, password);
                    ApplicationUser applicationUser;
                    if (!checkPassword)
                        return BadRequest("Pasword doesn't match!");
                    else
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        applicationUser = new ApplicationUser
                        {
                            Id = user.Id,
                            Email = user.Email,
                            UserName = user.UserName,
                            Role = roles?.FirstOrDefault(),
                        };
                        applicationUser.Token = GenerateJSONWebToken(applicationUser);
                    }
                    return Ok(applicationUser);
                }
                else
                    return NotFound("You are not authorized to sign in!");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private string GenerateJSONWebToken(ApplicationUser appUser)
        {
            var claimsIdentity = new ClaimsIdentity();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, appUser.Email),
                new Claim(ClaimTypes.Name, appUser.UserName),
                new Claim(ClaimTypes.NameIdentifier, appUser.Id),
                new Claim(ClaimTypes.Role, appUser.Role),
                new Claim("UserId", appUser.Id)
            };
            claimsIdentity.AddClaims(claims);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Audience"],
              new ClaimsIdentity(claimsIdentity)?.Claims,
              expires: DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:Expires"])),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
