using System.Threading.Tasks;
using Monitoring.IntegrationTests.Dto;
using Monitoring.IntegrationTests.Dto.Response;
using NUnit.Framework;

namespace Monitoring.IntegrationTests.Api.Public
{
    [TestFixture]
    public class SiteInfoApiTest : BaseApiTest
    {
        private const string ApiUri = "api/public/SiteInfo";

        [Test]
        public async Task GetList_Positive()
        {
            var responseData = await SendGetAsync<ResponseResult<ListData<SiteInfo>>>(ApiUri, false);

            Assert.IsTrue(responseData.Success);
        }
    }
}
