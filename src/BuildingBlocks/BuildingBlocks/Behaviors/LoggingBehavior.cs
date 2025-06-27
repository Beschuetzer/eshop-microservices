using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions;

public class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handling Request={Request} - Response={Response} - RequestData={RequestData}",
             typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timer = new Stopwatch();
        timer.Start();
        var response = await next(cancellationToken);
        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
        {
            logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken}ms to complete. Response={Response}",
                typeof(TRequest).Name, typeof(TResponse).Name, timeTaken.TotalMilliseconds);
        }
        logger.LogInformation("[END] Handled Request={Request} - Response={Response}",
            typeof(TRequest).Name, typeof(TResponse).Name);
        return response;
    }
}