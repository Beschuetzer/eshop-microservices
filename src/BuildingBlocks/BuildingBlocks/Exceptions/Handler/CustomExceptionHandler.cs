using BuildingBlocks.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unhandled exception occurred at {time}: {Message}", DateTime.UtcNow, exception.Message);

        (string Detail, string title, int statusCode) details = exception switch
        {
            ValidationException => (
                exception.Message,
                 exception.GetType().Name,
                StatusCodes.Status400BadRequest
            ),
            BadRequestException => (
               exception.Message,
                exception.GetType().Name,
               StatusCodes.Status400BadRequest
           ),
            NotFoundException => (
               exception.Message,
                exception.GetType().Name,
               StatusCodes.Status404NotFound
           ),
            _ => (
                exception.Message,
                exception.GetType().Name,
                StatusCodes.Status500InternalServerError
            )
        };
        var problemDetails = new ProblemDetails
        {
            Title = details.title,
            Detail = details.Detail,
            Status = details.statusCode,
            Instance = context.Request.Path,
        };
        problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

        if (exception is ValidationException validationException)
        {
            // You can add additional details if needed, for example, the validation message
            problemDetails.Extensions.Add("ValidationErrorMessage", validationException.Message);
        }

        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}