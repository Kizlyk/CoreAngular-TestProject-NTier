using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Utilities.Models;

namespace ProductCatalog.Helpers
{
    public static class HttpContextExtensions
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return false;
        }
    }

    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            var mainException = context.Exception;
            var baseException = context.Exception.GetBaseException();
            if (baseException is CustomException)
            {
                mainException = baseException;
            }

            var message = mainException.Message;
            var innerMessage = baseException.Message != message ? baseException.Message : null;

            HttpStatusCode status;
            if (mainException is CustomException)
            {
                status = (HttpStatusCode)((CustomException)mainException).HttpStatusCode;
            }
            else
            {
                status = HttpStatusCode.InternalServerError;
            }
            context.HttpContext.Response.StatusCode = (int)status;
            context.Result = new JsonResult(new { Message = message, ExceptionMessage = innerMessage, StackTrace = context.Exception.StackTrace });
        }
    }
}
