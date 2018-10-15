using Microsoft.AspNetCore.Mvc;
using Monitoring.WebHost.Dto;

namespace Monitoring.WebHost.Controllers
{
    public class BaseController : Controller
    {
        protected ObjectResult Success(string message, object data)
            => new ObjectResult(new ResponseResult
            {
                Success = true,
                Message = message ?? string.Empty,
                Data = data
            });

        protected ObjectResult Success(string message = null)
            => Success(message, null);

        protected ObjectResult Success(object data)
            => Success(null, data);

        protected ObjectResult Fail(string message)
            => new ObjectResult(new ResponseResult
            {
                Success = false,
                Message = message ?? string.Empty,
            });
    }
}
