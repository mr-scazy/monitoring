using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monitoring.Data;
using Monitoring.Domain.Entities;
using Monitoring.Domain.Interfaces;

namespace Monitoring.Services
{
    public class UserManager : IUserManager
    {
        private readonly AppDbContext _appDbContext;

        public UserManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<User> FindByNameAsync(string username) 
            => _appDbContext.Set<User>()
                .FirstOrDefaultAsync(x => x.UserName == username);

        public Task CreateAsync(User user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckPasswordAsync(User user, string password)
        {
            throw new NotImplementedException();
        }
    }
}
