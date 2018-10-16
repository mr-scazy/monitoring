using Quartz;
using System;
using System.Threading.Tasks;

namespace Monitoring.Quartz.Jobs
{
    /// <summary>
    /// Работа с информацией по сайтам
    /// </summary>
    public class SiteInfoJob : BaseJob
    {
        protected override async Task OnExecuteAsync(IJobExecutionContext context, IServiceProvider serviceProvider)
        {
            await Task.CompletedTask;
        }
    }
}
