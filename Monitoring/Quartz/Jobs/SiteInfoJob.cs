using Quartz;
using System;
using System.Threading.Tasks;

namespace Monitoring.Quartz.Jobs
{
    public class SiteInfoJob : BaseJob
    {
        protected override async Task ExecuteAsync(IJobExecutionContext context, IServiceProvider serviceProvider)
        {
            await Task.CompletedTask;
        }
    }
}
