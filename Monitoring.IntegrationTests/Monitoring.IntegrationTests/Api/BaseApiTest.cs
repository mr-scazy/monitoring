using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Monitoring.IntegrationTests.Api
{
    public abstract class BaseApiTest
    {
        protected IConfiguration Configuration { get; } = BuildConfiguration();

        protected virtual string BaseUrl => Configuration["BaseUrl"];

        private static IConfigurationRoot BuildConfiguration()
            => new ConfigurationBuilder()
                .AddJsonFile("testsettings.json", reloadOnChange: true, optional: false)
                .AddJsonFile("testsettings.local.json", reloadOnChange: true, optional: true)
                .Build();

        protected Task<T> SendGetAsync<T>(string requestUri)
            => SendAsync<T>(client => client.GetAsync(requestUri));

        protected Task<T> SendPostAsync<T>(string requestUri, HttpContent httpContent)
            => SendAsync<T>(client => client.PostAsync(requestUri, httpContent));

        protected Task<T> SendPutAsync<T>(string requestUri, HttpContent httpContent) 
            => SendAsync<T>(client => client.PutAsync(requestUri, httpContent));

        protected Task<T> SendDeleteAsync<T>(string requestUri)
            => SendAsync<T>(client => client.DeleteAsync(requestUri));

        protected async Task<T> SendAsync<T>(Func<HttpClient, Task<HttpResponseMessage>> sendAsync)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                var responceMessage = await sendAsync(client);
                var json = await responceMessage.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<T>(json);
                return responseData;
            }
        }
    }
}

