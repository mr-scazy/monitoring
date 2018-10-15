using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monitoring.Data;
using Monitoring.Domain.Dto;
using Monitoring.Domain.Entities;
using Monitoring.Domain.Interfaces;

namespace Monitoring.Services
{
    public class SiteInfoService : ISiteInfoService
    {
        private readonly AppDbContext _appDbContext;

        public SiteInfoService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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
