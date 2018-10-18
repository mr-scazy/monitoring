using System.Net.Http;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;

namespace Monitoring.IntegrationTests.Http
{
    public class JsonContent : StringContent
    {
        public JsonContent(object obj) : this(obj, Encoding.UTF8)
        {
        }

        public JsonContent(object obj, Encoding encoding) : base(JsonConvert.SerializeObject(obj), encoding, MediaTypeNames.Application.Json)
        {
        }
    }
}
