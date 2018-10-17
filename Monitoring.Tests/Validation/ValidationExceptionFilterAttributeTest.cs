using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Monitoring.Validation;
using Xunit;

namespace Monitoring.Tests.Validation
{
    public class ValidationExceptionFilterAttributeTest
    {
        [Fact]
        public void OnException_Positive()
        {
            var validationException = new ValidationException();

            var exceptionContext = CreateExceptionContext(validationException);

            var validationExceptionFilterAttribute = new ValidationExceptionFilterAttribute();
            validationExceptionFilterAttribute.OnException(exceptionContext);

            Assert.True(exceptionContext.ExceptionHandled);
        }

        [Fact]
        public void OnException_Negative()
        {
            var validationException = new Exception();

            var exceptionContext = CreateExceptionContext(validationException);

            var validationExceptionFilterAttribute = new ValidationExceptionFilterAttribute();
            validationExceptionFilterAttribute.OnException(exceptionContext);

            Assert.False(exceptionContext.ExceptionHandled);
        }

        private static ExceptionContext CreateExceptionContext(Exception exception)
        {
            var httpContext = new DefaultHttpContext();
            var routeData = new RouteData();
            var actionDescriptor = new ActionDescriptor();
            var actionContext = new ActionContext(httpContext, routeData, actionDescriptor);

            var filters = new List<IFilterMetadata>();
            var exceptionContext = new ExceptionContext(actionContext, filters)
            {
                Exception = exception
            };

            return exceptionContext;
        }
    }
}
