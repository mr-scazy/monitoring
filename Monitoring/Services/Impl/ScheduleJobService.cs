using Microsoft.EntityFrameworkCore;
using Monitoring.Data;
using Monitoring.Domain.Entities;
using Monitoring.Quartz;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Monitoring.Services.Impl
{
    public class ScheduleJobService : IScheduleJobService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IScheduler _scheduler;

        public ScheduleJobService(AppDbContext appDbContext, IScheduler scheduler)
        {
            _appDbContext = appDbContext;
            _scheduler = scheduler;
        }

        public async Task<IList<Exception>> InitAsync()
        {
            var items = await _appDbContext.Set<ScheduleJob>().ToArrayAsync();

            var exeptions = new List<Exception>();

            foreach(var item in items)
            {
                try
                {
                    var jobType = Type.GetType($"Monitoring.Quartz.Jobs.{item.Job}");

                    if(jobType == null)
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
                catch(Exception e)
                {
                    exeptions.Add(e);
                }
            }

            return exeptions;
        }

        public async Task<IList<Exception>> ConfigureAsync(params ScheduleJob[] scheduleJobs)
        {
            var exeptions = new List<Exception>();

            foreach(var item in scheduleJobs)
            {
                try
                {
                    var jobType = Type.GetType($"Monitoring.Quartz.Jobs.{item.Job}");

                    if(jobType == null)
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
    }
}
