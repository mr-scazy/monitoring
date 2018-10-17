namespace Monitoring.IntegrationTests.Dto.Response
{
    /// <summary>
    /// Результат ответа
    /// </summary>
    public class ResponseResult<T>
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
        public T Data { get; set; }
    }
}
