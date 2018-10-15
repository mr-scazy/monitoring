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
        [MaxLength(256), Required]
        public string Name { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        [Display(Name = "URL")]
        [MaxLength(1024), Required]
        public string Url { get; set; }

        /// <summary>
        /// Доступный
        /// </summary>
        [Display(Name = "Доступный")]
        public bool IsAvailable { get; set; }
    }
}
