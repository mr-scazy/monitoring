using System.Threading.Tasks;
using Monitoring.IntegrationTests.Dto;
using Monitoring.IntegrationTests.Dto.Response;
using NUnit.Framework;

namespace Monitoring.IntegrationTests.Api.Admin
{
    [TestFixture]
    public class AccountApiTest : BaseApiTest
    {
        private const string ApiUri = "api/admin/Account";

        [TestCase("admin","admin")]
        public async Task GetToken_Positive(string username, string password)
        {
            var uri = $"{ApiUri}/token?username={username}&password={password}";
            var responseData = await SendGetAsync<ResponseResult<UserToken>>(uri, false);

            Assert.IsTrue(
                responseData.Success && 
                !string.IsNullOrEmpty(responseData.Data.AccessToken) && 
                responseData.Data.Username == username);
        }

        [TestCase("admin", "sdf8ujsidf88sd8fj8")]
        [TestCase("admin5458_23487", "admin")]
        public async Task GetToken_Negative(string username, string password)
        {
            var uri = $"{ApiUri}/token?username={username}&password={password}";
            var responseData = await SendGetAsync<ResponseResult<UserToken>>(uri);

            Assert.IsFalse(
                responseData.Success && 
                !string.IsNullOrEmpty(responseData.Data.AccessToken) &&
                responseData.Data.Username == username);
        }
    }
}
