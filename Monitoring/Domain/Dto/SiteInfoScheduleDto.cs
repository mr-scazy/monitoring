namespace Monitoring.Domain.Dto
{
    public class SiteInfoScheduleDto : SiteInfoDto
    {
        /// <summary>
        /// Интервал проверки
        /// </summary>
        public int Interval { get; set; }
    }
}
