using Monitoring.Data;
using Monitoring.Domain.Entities;
using Monitoring.Security;
using Monitoring.Services;
using Monitoring.Services.Impl;
using Moq;
using Xunit;

namespace Monitoring.Tests.Services
{
    public class UserManagerTest
    {
        private readonly Mock<AppDbContext> _mockDbContext = new Mock<AppDbContext>();

        [Fact]
        public void CheckPassword_Positive()
        {
            const string password = "123";

            IUserManager userManager = new UserManager(_mockDbContext.Object);
            var user = new User { PasswordHash = SHA512Helper.GetHash(password) };
            var isValid = userManager.CheckPassword(user, password);

            Assert.True(isValid);
        }

        [Fact]
        public void CheckPassword_Negative()
        {
            const string password1 = "123";
            const string password2 = "111";

            IUserManager userManager = new UserManager(_mockDbContext.Object);
            var user = new User { PasswordHash = SHA512Helper.GetHash(password1) };
            var isValid = userManager.CheckPassword(user, password2);

            Assert.False(isValid);
        }
    }
}
