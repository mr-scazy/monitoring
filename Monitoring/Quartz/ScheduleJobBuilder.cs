using Monitoring.Domain.Entities;
using Quartz;
using System.Threading.Tasks;

namespace Monitoring.Quartz
{
    public class ScheduleJobBuilder
    {
        public async Task Build<TJob>(IScheduler scheduler) where TJob : IJob
        {
            var siteInfo = new SiteInfo();

            var map = new JobDataMap
            {
                ["Id"] = siteInfo.Id
            };


            //await scheduler.ScheduleJobTrigger<TJob>(new TriggerSettings { Name = siteInfo.Id.ToString(), Interval = siteInfo.}, map);
            //await scheduler.Start();

            //await scheduler.ConfigureTriggerAsync(new TriggerSettings { Name = siteInfo.Id.ToString(), IntervalInSeconds = 30});

        }
    }
}
