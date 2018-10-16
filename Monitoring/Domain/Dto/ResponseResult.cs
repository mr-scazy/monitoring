namespace Monitoring.Domain.Dto
{
    /// <summary>
    /// Результат ответа
    /// </summary>
    public class ResponseResult
    {
        /// <summary>
        /// Успешный результат
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Данные
        /// </summary>
        public object Data { get; set; }
    }
}
