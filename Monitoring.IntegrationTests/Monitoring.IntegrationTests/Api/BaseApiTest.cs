using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Monitoring.IntegrationTests.Dto;
using Monitoring.IntegrationTests.Dto.Response;
using Newtonsoft.Json;

namespace Monitoring.IntegrationTests.Api
{
    public abstract class BaseApiTest
    {
        protected IConfiguration Configuration { get; } = BuildConfiguration();

        protected virtual string BaseUrl => Configuration["BaseUrl"];

        protected virtual string UserName => "admin";

        protected virtual string Password => "admin";

        private UserToken _userToken;

        protected virtual UserToken UserToken 
            => _userToken ?? (_userToken = GetUserToken(UserName, Password).Result);

        protected async Task<UserToken> GetUserToken(string username, string password) 
            => (await SendGetAsync<ResponseResult<UserToken>>(
                $"api/admin/Account/token?username={username}&password={password}", false))?.Data;

        protected Task<T> SendGetAsync<T>(string requestUri, bool useToken = true)
            => SendAsync<T>(client => client.GetAsync(requestUri), useToken);

        protected Task<T> SendPostAsync<T>(string requestUri, HttpContent httpContent, bool useToken = true)
            => SendAsync<T>(client => client.PostAsync(requestUri, httpContent), useToken);

        protected Task<T> SendPutAsync<T>(string requestUri, HttpContent httpContent, bool useToken = true) 
            => SendAsync<T>(client => client.PutAsync(requestUri, httpContent), useToken);

        protected Task<T> SendDeleteAsync<T>(string requestUri, bool useToken = true)
            => SendAsync<T>(client => client.DeleteAsync(requestUri), useToken);

        protected async Task<T> SendAsync<T>(Func<HttpClient, Task<HttpResponseMessage>> sendAsync, bool useToken)
        {
            using (var client = new HttpClient())
            {
                if (useToken)
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {UserToken.AccessToken}");
                }

                client.BaseAddress = new Uri(BaseUrl);
                var responceMessage = await sendAsync(client);
                var json = await responceMessage.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<T>(json);
                return responseData;
            }
        }

        private static IConfigurationRoot BuildConfiguration()
            => new ConfigurationBuilder()
                .AddJsonFile("testsettings.json", reloadOnChange: true, optional: false)
                .AddJsonFile("testsettings.local.json", reloadOnChange: true, optional: true)
                .Build();
    }
}

