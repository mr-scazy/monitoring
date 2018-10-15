using Quartz;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Monitoring.Services;
using Quartz.Impl;

namespace Monitoring.Quartz
{
    public static class QuartzExtensions
    {
        /// <summary>
        /// Зарегистрировать реализацию <see cref="ISchedulerFactory"/>
        /// </summary>
        public static IServiceCollection AddSchedulerFactory(this IServiceCollection services) =>
            services.AddSingleton<ISchedulerFactory>(provider
                => new StdSchedulerFactory(new NameValueCollection
                {
                    ["quartz.threadPool.threadCount"] = "5"
                }));

        /// <summary>
        /// Зарегистрировать реализацию <see cref="IScheduler"/>
        /// </summary>
        public static IServiceCollection AddScheduler(this IServiceCollection services, string schedName = null) =>
            services.AddScoped(provider =>
            {
                var rootServiceProvider = provider.GetService<IServiceProvider>();
                var factory = rootServiceProvider.GetService<ISchedulerFactory>();
                if (factory == null)
                {
                    throw new NullReferenceException("Не найдена реализация ISchedulerFactory в IServiceProvider.");
                }

                var scheduler = string.IsNullOrEmpty(schedName)
                    ? factory.GetScheduler().Result
                    : factory.GetScheduler(schedName).Result;

                scheduler.ListenerManager.AddJobListener(new DIJobListener(rootServiceProvider));

                return scheduler;
            });

        /// <summary>
        /// Использовать Quartz
        /// </summary>
        public static IApplicationBuilder UseQuartz(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                scope.ServiceProvider
                    .GetService<IScheduleJobService>()
                    .InitAsync()?
                    .Wait();
            }

            return app;
        }

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

        public static IReadOnlyList<Type> GetQuartzJobTypes(this Assembly assembly)
        {
            var types = assembly
                .GetTypes()
                .Where(x => !x.IsAbstract && !x.IsGenericType && x.IsClass)
                .Where(x => x.GetInterfaces().Any(z => z == typeof(IJob)))
                .ToArray();

            return types;
        }
    }
}
