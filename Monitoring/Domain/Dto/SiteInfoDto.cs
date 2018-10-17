using System;
using Monitoring.Domain.Base;

namespace Monitoring.Domain.Dto
{
    /// <summary>
    ///  Информация по сайту
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

        /// <summary>
        /// Время обновления статуса
        /// </summary>
        public DateTime? StatusUpdateTime { get; set; }

        /// <summary>
        /// Время обновления статуса строкой
        /// </summary>
        public string StatusUpdateTimeString => StatusUpdateTime.HasValue 
            ? $"{StatusUpdateTime.Value:dd.MM.yyyy HH:mm}" 
            : string.Empty;
    }
}
