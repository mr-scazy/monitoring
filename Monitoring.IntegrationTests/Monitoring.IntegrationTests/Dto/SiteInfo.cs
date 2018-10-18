using System;

namespace Monitoring.IntegrationTests.Dto
{
    public class SiteInfo : LongIdBase
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

        /// <summary>
        /// Время обновления статуса
        /// </summary>
        public DateTime? StatusUpdateTime { get; set; }

        /// <summary>
        /// Время обновления статуса строкой
        /// </summary>
        public string StatusUpdateTimeString { get; set; }
    }
}
