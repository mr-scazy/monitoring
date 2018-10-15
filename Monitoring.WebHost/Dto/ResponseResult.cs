namespace Monitoring.WebHost.Dto
{
    /// <summary>
    /// Результат ответа
    /// </summary>
    public class ResponseResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
