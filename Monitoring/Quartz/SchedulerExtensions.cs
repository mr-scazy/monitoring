using Quartz;
using System;
using System.Threading.Tasks;

namespace Monitoring.Quartz
{
    public static class SchedulerExtensions
    {
        public static async Task<bool> ConfigureTriggerAsync(this IScheduler scheduler, TriggerSettings settings)
        {
            var triggerKey = new TriggerKey(settings.Name);

            var trigger = await scheduler.GetTrigger(triggerKey);

            if (trigger == null)
            {
                return false;
            }

            await scheduler.PauseTrigger(trigger.Key);

            trigger.GetTriggerBuilder()
                .SetInterval(settings)
                .Build();

            await scheduler.ResumeTrigger(trigger.Key);

            return true;
        }

        public static Task ScheduleJobTrigger<TJob>(this IScheduler scheduler, TriggerSettings settings, JobDataMap map = null) where TJob : IJob
            => scheduler.ScheduleJobTrigger(typeof(TJob), settings, map);
         

        public static async Task ScheduleJobTrigger(this IScheduler scheduler, Type jobType, TriggerSettings settings, JobDataMap map = null)
        {
            var trigger = TriggerBuilder.Create()                
                .WithIdentity(settings.Name)
                .SetInterval(settings)
                .StartNow()
                .Build();

            var jobDetail = JobBuilder.Create(jobType)
                .UsingJobData(map ?? new JobDataMap())
                .WithIdentity(settings.Name)
                .Build(); 

            await scheduler.ScheduleJob(jobDetail, trigger);
        }

        public static TriggerBuilder SetInterval(this TriggerBuilder triggerBuilder, TriggerSettings settings)
        {
            switch(settings.IntervalUnit)
            {
                case IntervalUnit.Millisecond:
                    return triggerBuilder.WithSimpleSchedule(builder 
                        => builder.WithInterval(TimeSpan.FromMilliseconds(settings.Interval)));

                case IntervalUnit.Second:
                    return triggerBuilder.WithSimpleSchedule(builder 
                        => builder.WithIntervalInSeconds(settings.Interval));

                case IntervalUnit.Minute:
                    return triggerBuilder.WithSimpleSchedule(builder 
                        => builder.WithIntervalInMinutes(settings.Interval));

                default: 
                    throw new NotImplementedException();
            }
        }
    }
}
