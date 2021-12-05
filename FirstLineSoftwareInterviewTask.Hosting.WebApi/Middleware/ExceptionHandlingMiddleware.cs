using System;
using System.Linq;
using System.Threading.Tasks;
using FirstLineSoftwareInterviewTask.Common.Core.Common.Error;
using FirstLineSoftwareInterviewTask.Common.Core.Common.Exceptions;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ValidationException = FluentValidation.ValidationException;
#pragma warning disable 1591

namespace FirstLineSoftwareInterviewTask.Hosting.WebApi.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly ICommandResultFactory _commandResultFactory;
        private readonly IJsonSerializer _jsonSerializer;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger,
            ICommandResultFactory commandResultFactory, 
            IJsonSerializer jsonSerializer)
        {
            _logger = logger;
            _commandResultFactory = commandResultFactory;
            _jsonSerializer = jsonSerializer;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = GetStatusCode(exception);

            var notificationMessage = GetNotificationMessage(exception);
            
            var commandResult = _commandResultFactory.Create(false, statusCode, notificationMessage, GetErrors(exception));

            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsync(_jsonSerializer.Serialize(commandResult));
        }

        private static string GetNotificationMessage(Exception exception)
            => exception switch
            {
                ValidationException => "Validation failed",
                _ => exception.Message
            };

        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };
        
        private static ErrorResponse GetErrors(Exception exception)
        {
            var errors = new ErrorResponse();

            if (exception is ValidationException validationException)
            {
                errors.Errors = validationException.Errors.Select(x => new ErrorItem()
                {
                    Field = x.PropertyName,
                    Message = x.ErrorMessage
                }).ToList();
            }

            return errors;
        }
    }
}