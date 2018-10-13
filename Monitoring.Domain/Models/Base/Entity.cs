using System.ComponentModel.DataAnnotations;
using Monitoring.Domain.Interfaces;

namespace Monitoring.Domain.Models.Base
{
    /// <summary>
    /// Базовая сущность
    /// </summary>
    public abstract class Entity : IEntity, IEntity<long>
    {
        /// <inheritdoc />
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Display(Name = "Идентификатор")]
        [Key]
        public virtual long Id { get; set; }

        object IEntity.Id => Id;
    }
}
