using Monitoring.Domain.Dto.Base;

namespace Monitoring.Domain.Dto
{
    /// <summary>
    /// Сайт
    /// </summary>
    public class SiteInfoDto : LongIdBase
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Доступный
        /// </summary>
        public bool IsAvailable { get; set; }
    }
}
