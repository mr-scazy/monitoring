namespace Monitoring.WebHost.Dto
{
    /// <summary>
    /// Форма с логином/паролем
    /// </summary>
    public class PwForm
    {
        /// <summary>
        /// Имя пользователя (логин)
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
