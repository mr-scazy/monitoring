using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monitoring.Data;
using Monitoring.Domain.Dto;
using Monitoring.Domain.Entities;
using Monitoring.Quartz.Jobs;
using Newtonsoft.Json;
using Quartz;

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

            return ListDataResult.Result(items, total);
        }

        public async Task<ListDataResult<SiteInfoScheduleDto>> GetListResultAsync()
        {
            var items = await _appDbContext.SiteInfos
                .Select(x => new SiteInfoScheduleDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Url = x.Url,
                    IsAvailable = x.IsAvailable
                })
                .ToListAsync();

            var total = items.Count;

            return ListDataResult.Result(items, total);
        }

        public async Task<SiteInfo> CreateAsync(SiteInfoScheduleDto dto)
        {
            var entity = new SiteInfo
            {
                Name = dto.Name,
                Url = dto.Url
            };

            await _appDbContext.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();

            var @params = new Dictionary<string, object>
            {
                ["SiteInfoId"] = entity.Id
            };

            var scheduleJob = new ScheduleJob
            {
                Name = $"{nameof(SiteInfoJob)}_{entity.Id}",
                Job = nameof(SiteInfoJob),
                Params = JsonConvert.SerializeObject(@params),
                Interval = dto.Interval,
                IntervalUnit = IntervalUnit.Second,
                UpdatedAt = DateTime.UtcNow
            };

            await _appDbContext.AddAsync(scheduleJob);
            await _appDbContext.SaveChangesAsync();

            return entity;
        }
    }
}
