using System.Net;
using System.Text.Json;
using EducationBySubscription.Api.Abstractions;
using EducationBySubscription.Application.Exceptions;
using EducationSubscription.Core.Primitives;

namespace EducationBySubscription.Api.Middlewares;

/// <summary>
/// Represents a global exception handler.
/// </summary>
public class ExceptionHandler : IMiddleware
{

    private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(ILogger<ExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (CustomValidationException exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var errorsList = new List<Error>();
            foreach (var errors in exception.Errors)
            {
                errorsList.Add(new Error("Server.ValidationError", errors.Message));
            }
            var apiErrorResponse =
                new ApiErrorResponse(errorsList.ToArray());
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var json = JsonSerializer.Serialize(apiErrorResponse, options);
            await context.Response.WriteAsync(json);
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            _logger.Log(LogLevel.Debug, "Stack trace: {0}",exception.StackTrace);
            _logger.Log(LogLevel.Error, "Exception message: {0}", exception.Message);
            _logger.Log(LogLevel.Error, "Inner exception: {0}", exception.InnerException?.Message);
            var apiErrorResponse = new ApiErrorResponse(new[] { new Error("Server.UnknownError", "An unexpected error occured.")});
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var json = JsonSerializer.Serialize(apiErrorResponse, options);
            await context.Response.WriteAsync(json);
        }
    }
}