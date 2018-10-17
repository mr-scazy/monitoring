using System;
using System.Threading.Tasks;
using Monitoring.Quartz;
using Quartz;

namespace Monitoring.Tests.Fakes.Quartz
{
    public class FakeJob : BaseJob
    {
        protected override Task OnExecuteAsync(IJobExecutionContext context, IServiceProvider serviceProvider)
        {
            return Task.CompletedTask;
        }
    }
}
