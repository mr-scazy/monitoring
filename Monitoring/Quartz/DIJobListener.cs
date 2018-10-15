using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Quartz;

namespace Monitoring.Quartz
{
    public class DIJobListener : IJobListener
    {
        public string Name => nameof(DIJobListener);

        private readonly IServiceProvider _rootServiceProvider;

        public DIJobListener(IServiceProvider rootServiceProvider)
        {
            _rootServiceProvider = rootServiceProvider;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            var job = context.JobInstance;

            var prop = job.GetType()
                .GetProperty(nameof(BaseJob.RootServiceProvider), 
                    BindingFlags.Instance | 
                    BindingFlags.Public | 
                    BindingFlags.GetProperty);

            prop.SetValue(job, _rootServiceProvider);

            return Task.CompletedTask;
        }

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.CompletedTask;
        }
    }
}
