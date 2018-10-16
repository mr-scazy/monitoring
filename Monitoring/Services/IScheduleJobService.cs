using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Monitoring.Domain.Entities;
using Monitoring.Domain.Interfaces;
using Monitoring.Domain.Services;
using Quartz;

namespace Monitoring.Services
{
    /// <summary>
    /// Интерфейс сервиса планируемой работы
    /// </summary>
    public interface IScheduleJobService
    {
        /// <summary>
        /// Инициализация планирования работ
        /// </summary>
        Task<IList<Exception>> InitAsync();

        /// <summary>
        /// Конфигурирование планирования работ
        /// </summary>
        /// <param name="scheduleJobs">Планируемые работы</param>
        Task<IList<Exception>> ConfigureAsync(params ScheduleJob[] scheduleJobs);

        /// <summary>
        /// Добавить планируемую работу в БД
        /// </summary>
        /// <typeparam name="T">Тип объекта, реализующий <see cref="IHasId"/> и <see cref="IHasInterval"/></typeparam>
        /// <param name="dto">Объект, реализующий <see cref="IHasId"/> и <see cref="IHasInterval"/></param>
        /// <param name="jobName"></param>
        Task<ScheduleJob> AddScheduleJobAsync<T>(T dto, string jobName) where T : class, IHasId, IHasInterval;

        /// <summary>
        /// Получить тип работы по имени типа
        /// </summary>
        /// <param name="name">Имя типа работы</param>
        Type GetJobType(string name);

        /// <summary>
        /// Получить имя планируемой работы
        /// </summary>
        /// <typeparam name="TJob">Тип работы</typeparam>
        /// <param name="postfix">Постфикс</param>
        string GetScheduleJobName<TJob>(string postfix) where TJob : class, IJob;

        /// <summary>
        /// Получить планируемую работу
        /// </summary>
        /// <typeparam name="TJob">Тип работы</typeparam>
        /// <param name="postfix">Постфикс</param>
        Task<ScheduleJob> GetScheduleJobAsync<TJob>(string postfix) where TJob : class, IJob;
    }
}
