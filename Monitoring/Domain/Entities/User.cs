﻿using System.ComponentModel.DataAnnotations;
using Monitoring.Domain.Base;

namespace Monitoring.Domain.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : LongIdBase
    {
        /// <summary>
        /// Имя (логин)
        /// </summary>
        [Display(Name = "Имя (логин)")]
        [Required(AllowEmptyStrings = false), MaxLength(256)]
        public string UserName { get; set; }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        [Display(Name = "Хэш пароля")]
        [Required(AllowEmptyStrings = false), MaxLength(512)]
        public string PasswordHash { get; set; }
    }
}
