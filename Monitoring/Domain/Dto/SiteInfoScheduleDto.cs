using Monitoring.Domain.Interfaces;
using Quartz;

namespace Monitoring.Domain.Dto
{
    /// <summary>
    /// Информаци по планируемым сайтам
    /// </summary>
    public class SiteInfoScheduleDto : SiteInfoDto, IHasInterval
    {
        /// <summary>
        /// Интервал проверки
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// Единица интервала
        /// </summary>
        public IntervalUnit IntervalUnit { get; set; }
    }
}
