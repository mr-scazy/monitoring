using Monitoring.IntegrationTests.Enums;

namespace Monitoring.IntegrationTests.Dto
{
    class SiteInfoSchedule : SiteInfo
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
