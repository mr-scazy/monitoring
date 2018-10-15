using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Monitoring.Domain.Entities;

namespace Monitoring.Domain.Interfaces
{
    public interface IUserManager
    {
        Task<User> FindByNameAsync(string username);

        Task CreateAsync(User user, string password);

        Task<bool> CheckPasswordAsync(User user, string password);
    }
}
