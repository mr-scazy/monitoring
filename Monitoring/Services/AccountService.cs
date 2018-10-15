using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Monitoring.Domain.Entities;
using Monitoring.Domain.Interfaces;

namespace Monitoring.Services
{
    /// <summary>
    /// Сервис работы с аккаунтом пользователя
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;

        public AccountService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Получить личную информацию пользователя
        /// </summary>
        public async Task<ClaimsIdentity> GetIdentityAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return null;
            }

            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                return null;
            }

            var claims = new []
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Token", 
                ClaimsIdentity.DefaultNameClaimType, 
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}
