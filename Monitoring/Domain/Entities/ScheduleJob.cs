using Monitoring.Domain.Base;
using Quartz;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Monitoring.Domain.Interfaces;

namespace Monitoring.Domain.Entities
{
    /// <summary>
    /// Планируемая работа
    /// </summary>
    [DisplayName("Планируемая работа")]
    public class ScheduleJob : LongIdBase, IHasInterval
    {
        /// <summary>
        /// Наименование
        /// </summary>
        [Display(Name = "Наименование")]
        [Required(AllowEmptyStrings = false), MaxLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// Работа
        /// </summary>
        [Display(Name = "Работа")]
        [Required(AllowEmptyStrings = false), MaxLength(256)]
        public string Job { get; set; }

        /// <summary>
        /// Параметры
        /// </summary>
        [Display(Name = "Параметры")]
        [Required(AllowEmptyStrings = false), MaxLength(4096)]
        public string Params { get; set; }

        /// <summary>
        /// Интервал
        /// </summary>
        [Display(Name = "Интервал")]
        public int Interval { get; set; }

        /// <summary>
        /// Единица измерения интервала
        /// </summary>
        [Display(Name = "Единица измерения интервала")]
        public IntervalUnit IntervalUnit { get; set; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        [Display(Name = "Дата обновления")]
        public DateTime UpdatedAt { get; set; }
    }
}
