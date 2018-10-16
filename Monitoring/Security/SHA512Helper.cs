using System;
using System.Security.Cryptography;
using System.Text;

namespace Monitoring.Security
{
    /// <summary>
    /// Вспомагательный класс для работы с <see cref="SHA512"/>
    /// </summary>
    public static class SHA512Helper
    {
        /// <summary>
        /// Получить хеш строки
        /// </summary>
        /// <param name="text">Строка, для которой будет сформирован хеш</param>
        /// <returns>Хеш</returns>
        public static string GetHash(string text)        
        {
            using(var sha512 = SHA512.Create())
            {
                var bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(text));
                var hash = Convert.ToBase64String(bytes);
                return hash;
            }
        }
    }
}
