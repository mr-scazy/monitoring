using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Monitoring.Services;

namespace Monitoring.WebHost.Controllers.Public
{
    [Route("api/public/[controller]")]
    public class SiteInfoController : BaseController
    {
        private readonly ISiteInfoService _siteInfoService;

        public SiteInfoController(ISiteInfoService siteInfoService)
        {
            _siteInfoService = siteInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _siteInfoService.GetDtoListResultAsync();

            return Success(data);
        }
    }
}
