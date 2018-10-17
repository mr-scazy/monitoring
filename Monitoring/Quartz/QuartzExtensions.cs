using Quartz;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Monitoring.Services;

namespace Monitoring.Quartz
{
    /// <summary>
    /// Класс методов-расширений для работы с Quartz
    /// </summary>
    public static class QuartzExtensions
    {
        /// <summary>
        /// Зарегистрировать реализацию <see cref="ISchedulerFactory"/>
        /// </summary>
        public static IServiceCollection AddSchedulerFactory(this IServiceCollection services) =>
            services.AddSingleton<ISchedulerFactory>(provider
                => new CustomSchedulerFactory(provider, new NameValueCollection
                {
                    ["quartz.threadPool.threadCount"] = "5"
                }));

        /// <summary>
        /// Зарегистрировать реализацию <see cref="IScheduler"/>
        /// </summary>
        public static IServiceCollection AddScheduler(this IServiceCollection services, string schedName = null) =>
            services.AddScoped(provider =>
            {
                var factory = provider.GetService<ISchedulerFactory>();
                if (factory == null)
                {
                    throw new NullReferenceException("Не найдена реализация ISchedulerFactory в IServiceProvider.");
                }

                var scheduler = string.IsNullOrEmpty(schedName)
                    ? factory.GetScheduler().Result
                    : factory.GetScheduler(schedName).Result;

                var customFactory = (CustomSchedulerFactory) factory;

                scheduler.ListenerManager.AddJobListener(new DIJobListener(customFactory.ServiceProvider));
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

        /// <summary>
        /// Конфигурирование триггера
        /// </summary>
        /// <param name="scheduler">Планировщик</param>
        /// <param name="settings">Настройки триггера</param>
        /// <returns>Асинхронная операция успешности конфигурирования</returns>
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
            if (!scheduler.IsStarted)
            {
                await scheduler.Start(CancellationToken.None);
            }

            return true;
        }

        /// <summary>
        /// Запланировать работу
        /// </summary>
        /// <param name="scheduler">Планировщик</param>
        /// <param name="jobType">Тип работы</param>
        /// <param name="settings">Настройки триггера</param>
        /// <param name="map">Параметры для работы</param>
        public static async Task ScheduleJobTrigger(this IScheduler scheduler, Type jobType, TriggerSettings settings, JobDataMap map = null)
        {
            var trigger = TriggerBuilder.Create().StartNow()
                .WithIdentity(settings.Name)
                .SetInterval(settings)
                .Build();

            var jobDetail = JobBuilder.Create(jobType)
                .UsingJobData(map ?? new JobDataMap())
                .WithIdentity(settings.Name)
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
            if (!scheduler.IsStarted)
            {
                await scheduler.Start(CancellationToken.None);
            }
        }

        /// <summary>
        /// Утановить интервал для триггера
        /// </summary>
        /// <param name="triggerBuilder">Билдер триггера</param>
        /// <param name="settings">Настройки триггера</param>
        /// <returns>Билдер триггера</returns>
        public static TriggerBuilder SetInterval(this TriggerBuilder triggerBuilder, TriggerSettings settings)
            => triggerBuilder.WithSimpleSchedule(builder =>
            {
                switch (settings.IntervalUnit)
                {
                    case IntervalUnit.Millisecond:
                        builder.WithInterval(TimeSpan.FromMilliseconds(settings.Interval));
                        break;

                    case IntervalUnit.Second:
                        builder.WithIntervalInSeconds(settings.Interval);
                        break;

                    case IntervalUnit.Minute:
                        builder.WithIntervalInMinutes(settings.Interval);
                        break;

                    case IntervalUnit.Hour:
                        builder.WithIntervalInHours(settings.Interval);
                        break;

                    case IntervalUnit.Day:
                        builder.WithInterval(TimeSpan.FromDays(settings.Interval));
                        break;

                    case IntervalUnit.Week:
                        builder.WithInterval(TimeSpan.FromDays(settings.Interval * 7));
                        break;

                    case IntervalUnit.Month:
                        builder.WithInterval(TimeSpan.FromDays(settings.Interval * 30));
                        break;

                    case IntervalUnit.Year:
                        builder.WithInterval(TimeSpan.FromDays(settings.Interval * 365));
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                builder.RepeatForever();
            });


        /// <summary>
        /// Получить все типы реализаций <see cref="IJob"/> из <see cref="Assembly"/>
        /// </summary>
        /// <param name="assembly">Сборка</param>
        /// <returns>Список типов реализаций <see cref="IJob"/></returns>
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
