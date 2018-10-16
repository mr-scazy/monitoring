using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monitoring.Data;
using Monitoring.Domain.Entities;
using Monitoring.Security;

namespace Monitoring.Services.Impl
{
    /// <summary>
    /// Менеджер пользователей
    /// </summary>
    public class UserManager : IUserManager
    {
        private readonly AppDbContext _appDbContext;

        public UserManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /// <summary>
        /// Найти пользователя по имени пользователя (логину)
        /// </summary>
        public async Task<User> FindByNameAsync(string username, CancellationToken cancellationToken = default(CancellationToken))
        {
            username = username ?? throw new ArgumentNullException(nameof(username));

            var user = await _appDbContext.Set<User>().FirstOrDefaultAsync(x => x.UserName == username, cancellationToken);
            return user;
        }

        /// <summary>
        /// Создать пользователя
        /// </summary>
        public async Task CreateAsync(User user, string password, CancellationToken cancellationToken = default(CancellationToken))
        {
            user = user ?? throw new ArgumentNullException(nameof(user));
            password = password ?? throw new ArgumentNullException(nameof(password));

            user.PasswordHash = SHA512Helper.GetHash(password);
            await _appDbContext.AddAsync(user, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Проверить пароль пользователя
        /// </summary>
        bool IUserManager.CheckPassword(User user, string password) 
            => CheckPassword(user, password);
        
        private static bool CheckPassword(User user, string password)
        {
            var hash = SHA512Helper.GetHash(password);
            var isValid = user.PasswordHash == hash;
            return isValid;
        }
    }
}
