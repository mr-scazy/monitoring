using Microsoft.EntityFrameworkCore;
using Monitoring.Data;
using Monitoring.Domain.Entities;
using Monitoring.Quartz;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Monitoring.Domain.Interfaces;
using Monitoring.Domain.Services;

namespace Monitoring.Services.Impl
{
    /// <summary>
    /// Сервис планируемой работы
    /// </summary>
    public class ScheduleJobService : IScheduleJobService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IScheduler _scheduler;

        private static readonly IReadOnlyList<Type> JobTypes = Assembly
            .GetAssembly(typeof(ScheduleJobService))
            .GetQuartzJobTypes();

        public ScheduleJobService(AppDbContext appDbContext, IScheduler scheduler)
        {
            _appDbContext = appDbContext;
            _scheduler = scheduler;
        }

        /// <summary>
        /// Инициализация планирования работ
        /// </summary>
        public async Task<IList<Exception>> InitAsync()
        {
            var items = await _appDbContext.Set<ScheduleJob>().ToArrayAsync();

            var exeptions = new List<Exception>();

            foreach (var item in items)
            {
                try
                {
                    var jobType = GetJobType(item.Job);
                    if (jobType == null)
                    {
                        continue;
                    }

                    var @params = JsonConvert.DeserializeObject<IDictionary>(item.Params);

                    var settings = new TriggerSettings
                    {
                        Name = item.Name,
                        Interval = item.Interval,
                        IntervalUnit = item.IntervalUnit
                    };

                    await _scheduler.ScheduleJobTrigger(jobType, settings, new JobDataMap(@params));
                }
                catch (Exception e)
                {
                    exeptions.Add(e);
                }
            }

            return exeptions;
        }

        /// <summary>
        /// Конфигурирование планирования работ
        /// </summary>
        public async Task<IList<Exception>> ConfigureAsync(params ScheduleJob[] scheduleJobs)
        {
            var exeptions = new List<Exception>();

            foreach(var item in scheduleJobs)
            {
                try
                {
                    var jobType = GetJobType(item.Job);
                    if (jobType == null)
                    {
                        continue;
                    }

                    var @params = JsonConvert.DeserializeObject<IDictionary>(item.Params);

                    var settings = new TriggerSettings
                    {
                        Name = item.Name,
                        Interval = item.Interval,
                        IntervalUnit = item.IntervalUnit
                    };

                    if (!await _scheduler.ConfigureTriggerAsync(settings))
                    {
                        await _scheduler.ScheduleJobTrigger(jobType, settings, new JobDataMap(@params));
                    }
                }
                catch(Exception e)
                {
                    exeptions.Add(e);
                }
            }

            return exeptions;
        }

        /// <summary>
        /// Добавить планируемую работу в БД
        /// </summary>
        public async Task<ScheduleJob> AddScheduleJobAsync<T>(T dto, string jobName) where T : class, IHasId, IHasInterval
        {
            var @params = new Dictionary<string, object>
            {
                ["SiteInfoId"] = dto.Id
            };

            var scheduleJob = new ScheduleJob
            {
                Name = $"{jobName}_{dto.Id}",
                Job = jobName,
                Params = JsonConvert.SerializeObject(@params),
                Interval = dto.Interval,
                IntervalUnit = dto.IntervalUnit,
                UpdatedAt = DateTime.UtcNow
            };

            await _appDbContext.AddAsync(scheduleJob);
            return scheduleJob;
        }

        /// <summary>
        /// Получить тип работы по имени типа
        /// </summary>
        Type IScheduleJobService.GetJobType(string name) 
            => GetJobType(name);

        /// <summary>
        /// Получить имя планируемой работы
        /// </summary>
        string IScheduleJobService.GetScheduleJobName<TJob>(string postfix)
            => GetScheduleJobName<TJob>(postfix);

        /// <summary>
        /// Получить планируемую работу
        /// </summary>
        public async Task<ScheduleJob> GetScheduleJobAsync<TJob>(string postfix) where TJob : class, IJob
        {
            var scheduleJobName = GetScheduleJobName<TJob>(postfix);
            var scheduleJob = await _appDbContext.Set<ScheduleJob>().FirstOrDefaultAsync(x => x.Name == scheduleJobName);
            return scheduleJob;
        }

        private static Type GetJobType(string name) 
            => JobTypes.FirstOrDefault(x => x.Name == name);

        private static string GetScheduleJobName<TJob>(string postfix) where TJob : class, IJob
            => $"{typeof(TJob).Name}_{postfix}";
    }
}