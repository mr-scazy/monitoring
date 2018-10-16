using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Monitoring.Domain.Dto;
using Monitoring.Domain.Entities;
using Monitoring.Services;

namespace Monitoring.WebHost.Controllers.Admin
{
    [Route("api/admin/[controller]")]
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
            var data = await _siteInfoService.GetListResultAsync();
            return Success(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SiteInfoScheduleDto dto)
        {
            var entity = await _siteInfoService.CreateAsync(dto);
            return CreatedAtAction("Get", new { entity.Id });
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] SiteInfo model)
        {
            return Success();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            return Success();
        }
    }
}
