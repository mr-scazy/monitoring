using System.Security.Claims;
using System.Threading.Tasks;

namespace Monitoring.Services.Impl
{
    /// <summary>
    /// Сервис работы с аккаунтом пользователя
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IUserManager _userManager;

        public AccountService(IUserManager userManager)
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

            if (!_userManager.CheckPassword(user, password))
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
