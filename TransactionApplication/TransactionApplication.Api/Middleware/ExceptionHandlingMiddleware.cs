using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TransactionApplication.Api.Models;
using TransactionApplication.Infrastructure.Constants;
using TransactionApplication.Infrastructure.Exceptions;

namespace TransactionApplication.Api.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {

        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var errorId = context.TraceIdentifier;

            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                HandleException(context, exception, errorId);
            }
        }

        private void HandleException(HttpContext context, Exception exception, string errorId)
        {
            _logger.LogError(exception, "Error ID: {errorId}", errorId);

            switch (exception)
            {
                case EntityNotFoundException:
                    SetErrorInBody(context, StatusCodes.Status404NotFound, exception.Message);
                    break;
                case ValidatonException:
                case FluentValidation.ValidationException:
                    SetErrorInBody(context, StatusCodes.Status400BadRequest, exception.Message);
                    break;
                default:
                    SetErrorInBody(context, StatusCodes.Status500InternalServerError, Constants.InternalErrorDefaultMessage);
                    break;
            }
        }

        private static void SetErrorInBody(HttpContext context, int statusCode, string errorMessage)
        {
            var body = JsonConvert.SerializeObject(
                new BadRequestBody
                {
                    StatusCode = statusCode,
                    Error = errorMessage,
                },
                Formatting.None);

            context.Response.WriteAsync(body);
        }
    }
}