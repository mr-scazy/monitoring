using System.Threading.Tasks;
using Monitoring.IntegrationTests.Dto;
using Monitoring.IntegrationTests.Dto.Response;
using NUnit.Framework;

namespace Monitoring.IntegrationTests.Api.Admin
{
    [TestFixture]
    public class AccountApiTest : BaseApiTest
    {
        [TestCase("admin","admin")]
        public async Task GetToken_Positive(string username, string password)
        {
            var uri = $"api/admin/Account/token?username={username}&password={password}";
            var responseData = await SendGetAsync<ResponseResult<TokenData>>(uri);
            Assert.IsTrue(responseData.Success && !string.IsNullOrEmpty(responseData.Data.AccessToken));
        }

        [TestCase("admin", "sdf8ujsidf88sd8fj8")]
        [TestCase("admin5458_23487", "admin")]
        public async Task GetToken_Negative(string username, string password)
        {
            var uri = $"api/admin/Account/token?username={username}&password={password}";
            var responseData = await SendGetAsync<ResponseResult<TokenData>>(uri);
            Assert.IsFalse(responseData.Success && !string.IsNullOrEmpty(responseData.Data.AccessToken));
        }
    }
}
