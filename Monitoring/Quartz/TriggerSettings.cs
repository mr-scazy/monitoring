using Monitoring.Domain.Interfaces;
using Quartz;

namespace Monitoring.Quartz
{
    /// <summary>
    /// Настройки триггера
    /// </summary>
    public class TriggerSettings : IHasInterval
    {
        /// <summary>
        /// Имя триггера
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Интервал
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// Единица измерения интервала
        /// </summary>
        public IntervalUnit IntervalUnit { get; set; }
    }
}
