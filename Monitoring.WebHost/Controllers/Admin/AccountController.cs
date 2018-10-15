using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Monitoring.Domain.Entities;
using Monitoring.Domain.Interfaces;
using Monitoring.WebHost.Dto;
using Monitoring.WebHost.Security;

namespace Monitoring.WebHost.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("/token")]
        public async Task<IActionResult> Token([FromForm] PwForm form)
        {
            var identity = await _accountService.GetIdentityAsync(form.Username, form.Password);
            if (identity == null)
            {
                return BadRequest("Invalid username or password.");
            }

            var jwt = JwtSecurity.CreateToken(identity.Claims);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Success(response);
        }

        [HttpGet("init")]
        public async Task<IActionResult> Init([FromServices] UserManager<User> userManager)
        {
            var user = userManager.FindByNameAsync("admin");
            if (user != null)
            {
                return NotFound();
            }

            var result = await userManager.CreateAsync(
                new User
                {
                    UserName = "admin"
                }, 
                "admin");

            return Success(result);
        }
    }
}
