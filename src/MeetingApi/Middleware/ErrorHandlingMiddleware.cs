using MeetingApi.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace MeetingApi.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        private async Task WriteErrorMessage(string message, HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var error = JsonConvert.SerializeObject(new { Error = message });
            await context.Response.WriteAsync(error);
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (BadRequestException badRequestException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await WriteErrorMessage(badRequestException.Message, context);
            }
            catch (NotFoundException notFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await WriteErrorMessage(notFoundException.Message, context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await WriteErrorMessage("Something went wrong", context);
            }
        }
    }
}
