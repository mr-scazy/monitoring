using System.Threading;
using System.Threading.Tasks;
using Monitoring.Domain.Entities;

namespace Monitoring.Services
{
    /// <summary>
    /// Интерфейс менеджера пользователей
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Найти пользователя по имени пользователя (логину)
        /// </summary>
        /// <param name="username">Имя пользователя (логина)</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        Task<User> FindByNameAsync(string username, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Создать пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="password">Пароль</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        Task CreateAsync(User user, string password, CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// Проверить пароль пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="password">Пароль</param>
        bool CheckPassword(User user, string password);
    }
}
