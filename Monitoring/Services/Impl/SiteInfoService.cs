using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monitoring.Data;
using Monitoring.Domain.Dto;
using Monitoring.Domain.Entities;
using Monitoring.Quartz.Jobs;
using Quartz;

namespace Monitoring.Services.Impl
{
    /// <summary>
    /// Сервис информации по сайтам
    /// </summary>
    public class SiteInfoService : ISiteInfoService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IScheduleJobService _scheduleJobService;

        public SiteInfoService(AppDbContext appDbContext, IScheduleJobService scheduleJobService)
        {
            _appDbContext = appDbContext;
            _scheduleJobService = scheduleJobService;
        }

        /// <summary>
        /// Полуить резуьтат списка информации по сайтам
        /// </summary>
        public async Task<ListDataResult<SiteInfoDto>> GetSiteInfoDtoListAsync()
        {
            var items = await _appDbContext.SiteInfos
                .Select(x => new SiteInfoDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Url = x.Url,
                    IsAvailable = x.IsAvailable,
                    StatusUpdateTime = x.StatusUpdateTime
                })
                .OrderBy(x => x.Id)
                .ToListAsync();

            var total = items.Count;

            return ListDataResult.Result(items, total);
        }

        /// <summary>
        /// Полуить резуьтат списка информации по сайтам и планированию работы
        /// </summary>
        public async Task<ListDataResult<SiteInfoScheduleDto>> GetSiteInfoScheduleDtoListAsync()
        {
            var items = await _appDbContext.SiteInfos
                .Select(x => new SiteInfoScheduleDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Url = x.Url,
                    IsAvailable = x.IsAvailable
                })
                .OrderBy(x => x.Id)
                .ToListAsync();

            var total = items.Count;

            return ListDataResult.Result(items, total);
        }

        /// <summary>
        /// Создать запись информации по сайту
        /// </summary>
        public async Task<SiteInfoScheduleDto> CreateAsync(SiteInfoScheduleDto dto)
        {
            var entity = new SiteInfo
            {
                Name = dto.Name,
                Url = dto.Url
            };

            dto.Id = entity.Id;
            dto.IntervalUnit = IntervalUnit.Second;

            await _appDbContext.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();

            var scheduleJob = await _scheduleJobService.AddScheduleJobAsync(dto, nameof(SiteInfoJob));

            await _appDbContext.SaveChangesAsync();

            await _scheduleJobService.ConfigureAsync(scheduleJob);

            dto.Interval = scheduleJob.Interval;
            dto.IntervalUnit = scheduleJob.IntervalUnit;

            return dto;
        }

        /// <summary>
        /// Обновить запись информации по сайту
        /// </summary>
        public async Task<SiteInfoScheduleDto> UpdateAsync(SiteInfoScheduleDto dto)
        {
            var entity = await _appDbContext.Set<SiteInfo>().FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (entity == null)
            {
                throw new ValidationException("Ошибка обновления. Не найдена информация по сайту.");
            }

            entity.Name = dto.Name;
            entity.Url = dto.Url;

            //задаем ед. измерения в секундах
            dto.IntervalUnit = IntervalUnit.Second;
            
            var scheduleJob = await _scheduleJobService.GetScheduleJobAsync<SiteInfoJob>(entity.Id.ToString()) ??
                              await _scheduleJobService.AddScheduleJobAsync(dto, nameof(SiteInfoJob));

            await _appDbContext.SaveChangesAsync();

            await _scheduleJobService.ConfigureAsync(scheduleJob);

            return dto;
        }

        /// <summary>
        /// Удалить запись информации по сайту
        /// </summary>
        public async Task DeleteAsync(long id)
        {
            var entity = await _appDbContext.Set<SiteInfo>().FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new ValidationException("Ошибка удаления. Не найдена информация по сайту.");
            }

            _appDbContext.Remove(entity);

            var scheduleJob = await _scheduleJobService.GetScheduleJobAsync<SiteInfoJob>(entity.Id.ToString());
            if (scheduleJob != null)
            {
                _appDbContext.Remove(scheduleJob);
            }

            await _appDbContext.SaveChangesAsync();
        }
    }
}
