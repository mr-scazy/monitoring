using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Monitoring.Domain.Base;

namespace Monitoring.Domain.Entities
{
    /// <summary>
    /// Информация по сайтам
    /// </summary>
    [DisplayName("Информация по сайтам")]
    public class SiteInfo : LongIdBase
    {
        /// <summary>
        /// Наименование
        /// </summary>
        [Display(Name = "Наименование")]
        [Required(AllowEmptyStrings = false), MaxLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        [Display(Name = "URL")]
        [Required(AllowEmptyStrings = false), MaxLength(1024)]
        public string Url { get; set; }

        /// <summary>
        /// Доступный
        /// </summary>
        [Display(Name = "Доступный")]
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Время обновления статуса
        /// </summary>
        [Display(Name = "Время обновления статуса")]
        public DateTime? StatusUpdateTime { get; set; }
    }
}
