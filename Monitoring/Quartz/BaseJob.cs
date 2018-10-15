using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Monitoring.Quartz
{
    public abstract class BaseJob : IJob
    {
        public IServiceProvider RootServiceProvider { get; } = null;

        protected abstract Task ExecuteAsync(IJobExecutionContext context, IServiceProvider serviceProvider);

        Task IJob.Execute(IJobExecutionContext context)
        {
            using (var scope = RootServiceProvider.CreateScope())
            {
                return ExecuteAsync(context, scope.ServiceProvider);
            }
        }
    }
}
