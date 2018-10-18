using System.Threading.Tasks;
using Monitoring.IntegrationTests.Dto;
using Monitoring.IntegrationTests.Dto.Response;
using Monitoring.IntegrationTests.Enums;
using Monitoring.IntegrationTests.Http;
using NUnit.Framework;

namespace Monitoring.IntegrationTests.Api.Admin
{
    [TestFixture]
    public class SiteInfoScheduleApiTest : BaseApiTest
    {
        private const string ApiUri = "api/admin/SiteInfoSchedule";

        [Test]
        public async Task GetList_Positive()
        {
            var responseData = await SendGetAsync<ResponseResult<ListData<SiteInfoSchedule>>>(ApiUri);

            Assert.IsTrue(responseData.Success);
        }

        [TestCase("Хабр", "habr.com", 30)]
        public async Task Post_Positive(string name, string url, int interval)
        {
            var request = new SiteInfoSchedule
            {
                Name = name,
                Url = url,
                Interval = interval,
                IntervalUnit = IntervalUnit.Second
            };

            var responseData = await SendPostAsync<ResponseResult<LongIdBase>>(ApiUri, new JsonContent(request));

            Assert.IsTrue(responseData.Success && responseData.Data.Id > 0L);
        }

        [TestCase("", "habr.com", 30)]
        [TestCase("Хабр", "", 30)]
        [TestCase("", "", 30)]
        public async Task Post_Negative(string name, string url, int interval)
        {
            var request = new SiteInfoSchedule
            {
                Name = name,
                Url = url,
                Interval = interval,
                IntervalUnit = IntervalUnit.Second
            };

            var responseData = await SendPostAsync<ResponseResult<LongIdBase>>(ApiUri, new JsonContent(request));

            Assert.IsFalse(responseData.Success && responseData.Data.Id > 0L);
        }

        [TestCase("Хабр", "habr.com", 30)]
        public async Task Delete_Positive(string name, string url, int interval)
        {
            var request = new SiteInfoSchedule
            {
                Name = name,
                Url = url,
                Interval = interval,
                IntervalUnit = IntervalUnit.Second
            };

            var responseData = await SendPostAsync<ResponseResult<LongIdBase>>(ApiUri, new JsonContent(request));

            ResponseResult<object> responseDeleteData = null;

            if (responseData.Success && responseData.Data.Id > 0L)
            {
                responseDeleteData = await SendDeleteAsync<ResponseResult<object>>($"{ApiUri}/{responseData.Data.Id}");
            }

            Assert.IsTrue(responseDeleteData?.Success == true);
        }

        [TestCase("Хабр", "habr.com", 30)]
        public async Task Put_Positive(string name, string url, int interval)
        {
            var request = new SiteInfoSchedule
            {
                Name = name,
                Url = url,
                Interval = interval,
                IntervalUnit = IntervalUnit.Second
            };

            var responseData = await SendPostAsync<ResponseResult<LongIdBase>>(ApiUri, new JsonContent(request));

            ResponseResult<SiteInfoSchedule> responsePutData = null;
            
            if (responseData.Success && responseData.Data.Id > 0L)
            {
                request.Id = responseData.Data.Id;
                responsePutData = await SendPutAsync<ResponseResult<SiteInfoSchedule>>(ApiUri, new JsonContent(request));
            }

            var success = responsePutData?.Success == true &&
                          responsePutData.Data != null &&
                          responsePutData.Data.Name == request.Name &&
                          responsePutData.Data.Url == request.Url &&
                          responsePutData.Data.Interval == request.Interval &&
                          responsePutData.Data.IntervalUnit == request.IntervalUnit;

            Assert.IsTrue(success);
        }
    }
}
