using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Monitoring.Domain.Dto;

namespace Monitoring.Validation
{
    /// <summary>
    /// Фильтр <see cref="ValidationException"/>
    /// </summary>
    public class ValidationExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Обработка исключения
        /// </summary>
        /// <param name="context">Контекст исключения</param>
        public override void OnException(ExceptionContext context)
        {
            var contextException = context.Exception;

            var e = contextException as ValidationException;

            if (contextException is AggregateException ex)
            {
                if (ex.InnerExceptions?.Count == 1 &&
                    ex.InnerException is ValidationException ve)
                {
                    e = ve;
                }
            }

            if (e == null)
            {
                return;
            }

            context.Result = new ObjectResult(new ResponseResult { Message = e.Message });

            context.ExceptionHandled = true;
        }
    }
}
