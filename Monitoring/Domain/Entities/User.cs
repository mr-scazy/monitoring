using System.ComponentModel.DataAnnotations;
using Monitoring.Domain.Base;

namespace Monitoring.Domain.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : LongIdBase
    {
        [MaxLength(256), Required]
        public string UserName { get; set; }

        [MaxLength(512), Required]
        public string PasswordHash { get; set; }
    }
}
