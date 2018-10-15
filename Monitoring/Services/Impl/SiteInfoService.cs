using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monitoring.Data;
using Monitoring.Domain.Dto;
using Monitoring.Domain.Entities;

namespace Monitoring.Services.Impl
{
    public class SiteInfoService : ISiteInfoService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IScheduleJobService _scheduleJobService;

        public SiteInfoService(AppDbContext appDbContext, IScheduleJobService scheduleJobService)
        {
            _appDbContext = appDbContext;
            _scheduleJobService = scheduleJobService;
        }

        public async Task<ListDataResult<SiteInfoDto>> GetDtoListResultAsync()
        {
            var items = await _appDbContext.SiteInfos
                .Select(x => new SiteInfoDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Url = x.Url,
                    IsAvailable = x.IsAvailable
                })
                .ToListAsync();

            var total = items.Count;

            return ListDataResult.NewResult(items, total);
        }

        public async Task<ListDataResult<SiteInfo>> GetListResultAsync()
        {
            var items = await _appDbContext.SiteInfos.ToListAsync();
            var total = items.Count;

            return ListDataResult.NewResult(items, total);
        }
    }
}
