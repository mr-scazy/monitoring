using Microsoft.AspNetCore.Mvc;
using Monitoring.Domain.Dto;

namespace Monitoring.WebHost.Controllers
{
    /// <summary>
    /// Базовый контроллер
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Успешный результат
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="data">Данные</param>
        protected ObjectResult Success(string message, object data)
            => new ObjectResult(new ResponseResult
            {
                Success = true,
                Message = message ?? string.Empty,
                Data = data
            });

        /// <summary>
        /// Успешный результат
        /// </summary>
        /// <param name="message">Сообщение</param>
        protected ObjectResult Success(string message = null)
            => Success(message, null);

        /// <summary>
        /// Успешный результат
        /// </summary>
        /// <param name="data">Данные</param>
        protected ObjectResult Success(object data)
            => Success(null, data);

        /// <summary>
        /// Результат с ошибкой
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <returns></returns>
        protected ObjectResult Fail(string message)
            => new ObjectResult(new ResponseResult
            {
                Success = false,
                Message = message ?? string.Empty,
            });
    }
}
