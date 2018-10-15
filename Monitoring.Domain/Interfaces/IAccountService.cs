using System.Security.Claims;
using System.Threading.Tasks;
using Monitoring.Domain.Entities;

namespace Monitoring.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса работы с аккаунтом пользователя
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Получить личную информацию пользователя
        /// </summary>
        /// <param name="username">Имя пользователя (логин)</param>
        /// <param name="password">Пароль</param>
        Task<ClaimsIdentity> GetIdentityAsync(string username, string password);
    }
}
