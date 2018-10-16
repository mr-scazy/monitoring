using Quartz;

namespace Monitoring.Domain.Interfaces
{
    /// <summary>
    /// Интервейс наличия интервала
    /// </summary>
    public interface IHasInterval
    {
        /// <summary>
        /// Интервал
        /// </summary>
        int Interval { get; }

        /// <summary>
        /// Единица измерения интервала
        /// </summary>
        IntervalUnit IntervalUnit { get; }
    }
}
