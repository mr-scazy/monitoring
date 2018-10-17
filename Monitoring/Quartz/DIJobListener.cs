using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Monitoring.Quartz
{
    /// <summary>
    /// Слушатель событий исполнения работ для внедрения зависимостей
    /// </summary>
    public class DIJobListener : IJobListener
    {
        public string Name => nameof(DIJobListener);

        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Слушатель событий исполнения работ для внедрения зависимостей
        /// </summary>
        /// <param name="serviceProvider">Корневой сервис-провайдер</param>
        public DIJobListener(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            var job = context.JobInstance;

            await Task.Delay(10, cancellationToken);

            var prop = job.GetType()
                .GetProperty(nameof(BaseJob.ServiceProvider), 
                    BindingFlags.Instance | 
                    BindingFlags.Public);
            
            prop.SetValue(job, _serviceProvider);
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
