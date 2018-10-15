using System.ComponentModel.DataAnnotations;
using Monitoring.Domain.Services;

namespace Monitoring.Domain.Base
{
    /// <summary>
    /// Базовая класс с идентификатором типа <see cref="long"/>
    /// </summary>
    public abstract class LongIdBase : IHasId, IHasId<long>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Display(Name = "Идентификатор")]
        [Key]
        public virtual long Id { get; set; }

        object IHasId.Id => Id;
    }
}
