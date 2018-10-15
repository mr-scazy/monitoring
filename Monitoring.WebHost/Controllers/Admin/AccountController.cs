using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Monitoring.Domain.Entities;
using Monitoring.Services;
using Monitoring.Security;

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

        [HttpGet("token")]
        public async Task<IActionResult> Token(string username, string password)
        {
            var identity = await _accountService.GetIdentityAsync(username, password);
            if (identity == null)
            {
                return BadRequest("Invalid username or password.");
            }

            var jwt = JwtSecurity.CreateToken(identity.Claims);

            var access_token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Success(new { access_token });
        }

        [HttpGet("Init")]
        public async Task<IActionResult> Init([FromServices] IUserManager userManager)
        {
            var user = await userManager.FindByNameAsync("admin");
            if (user != null)
            {
                return NotFound();
            }

            await userManager.CreateAsync(new User { UserName = "admin" }, "admin");

            return Success();
        }
    }
}
