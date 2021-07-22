using System;
using System.Net;
using System.Threading.Tasks;
using CommonLibraries.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CommonLibraries.Web
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory?.CreateLogger<ErrorHandlingMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var originalExc = GetInnerException(ex);
                var message = $"Message: {originalExc.Message}, StackTrace: {originalExc.StackTrace}";
                _logger.LogError(message: message);

                await HandleExceptionAsync(context, originalExc);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            //if (exception is MyNotFoundException) code = HttpStatusCode.NotFound;
            //else if (exception is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            //else if (exception is MyException) code = HttpStatusCode.BadRequest;

            var result = (new { error = exception.Message, stack = exception.StackTrace }).Serialize();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private static Exception GetInnerException(Exception exc)
        {
            var maxCount = 50;
            var iterator = 0;
            while (exc.InnerException != null)
            {
                if (iterator >= maxCount)
                    return exc;

                iterator++;
                exc = exc.InnerException;
            }

            return exc;
        }
    }
}
