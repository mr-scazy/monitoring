using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Monitoring.Security
{
    public class AuthOptions
    {
        /// <summary>
        /// Издатель токена
        /// </summary>
        public const string Issuer = "MonitoringServer";

        /// <summary>
        /// Потребитель токена
        /// </summary>
        public const string Audience = "http://localhost:51884/";

        /// <summary>
        /// Ключ шифорования
        /// </summary>
        private const string Key = "LZv~9powXftbs*#m4xa7WHPOi*VMrnsbfBlv{Gm5KE|Noxps7h";

        /// <summary>
        /// Время жизни токена (в минутах)
        /// </summary>
        public const int Lifetime = 15;

        /// <summary>
        /// Получить секретный ключ
        /// </summary>
        /// <returns>Секретный ключ</returns>
        public static SymmetricSecurityKey GetSymmetricSecurityKey() 
            => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}
