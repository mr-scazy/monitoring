using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Monitoring.Quartz
{
    /// <summary>
    /// Базовая работа
    /// </summary>
    public abstract class BaseJob : IJob
    {
        /// <summary>
        /// Корневой сервис-провайдер
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// Выполнить работы
        /// </summary>
        /// <param name="context">Контекст выполняемой работы</param>
        /// <param name="serviceProvider">Сервис провайдер скоупа</param>
        protected abstract Task OnExecuteAsync(IJobExecutionContext context, IServiceProvider serviceProvider);

        Task IJob.Execute(IJobExecutionContext context)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                return OnExecuteAsync(context, scope.ServiceProvider);
            }
        }
    }
}
