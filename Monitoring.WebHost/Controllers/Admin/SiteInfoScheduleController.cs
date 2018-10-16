using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monitoring.Domain.Dto;
using Monitoring.Services;

namespace Monitoring.WebHost.Controllers.Admin
{
    [Route("api/admin/[controller]"), Authorize]
    public class SiteInfoScheduleController : BaseController
    {
        private readonly ISiteInfoService _siteInfoService;

        public SiteInfoScheduleController(ISiteInfoService siteInfoService)
        {
            _siteInfoService = siteInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _siteInfoService.GetSiteInfoScheduleDtoListAsync();
            return Success(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SiteInfoScheduleDto dto)
        {
            var entity = await _siteInfoService.CreateAsync(dto);
            return CreatedAtAction("Get", new { entity.Id });
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] SiteInfoScheduleDto dto)
        {
            var entity = await _siteInfoService.UpdateAsync(dto);
            return Success(entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (id <= 0L)
            {
                return Fail($"Invalid {nameof(id)}.");
            }

            await _siteInfoService.DeleteAsync(id);
            return Success();
        }
    }
}
