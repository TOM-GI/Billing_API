using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Billing_API.Common.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Billing_API.Middlewares
{
    //Log internal exception and hide it from end-user for safety reasons
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try { await _next(httpContext); }
            catch (Exception ex) { await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError); }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
        {
            var userIdentity = context.User.ExtractUserClaimsFromPrincipals();

            _logger.Log(LogLevel.Error,
                userIdentity is not null
                    ? $@"{nameof(userIdentity.UserId)}: {userIdentity.UserId}. 
                     Message: {exception.Message}. 
                     Stack Trace: {exception.StackTrace}."
                    : $@"Information on the identity of the customer cannot be obtained. 
                     Message: {exception.Message}. 
                     Stack Trace: {exception.StackTrace}.");

            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = context.Response.StatusCode,
                Title = exception.Message
            });
        }
    }
}