using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Shared.BuildingBlocks.Application.Behaviors;

public sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : IResult
{
    private readonly ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> _logger = logger;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        _logger.LogInformation("Processing request {RequestName}", requestName);

        var stopwatch = Stopwatch.StartNew();

        TResponse response = await next(cancellationToken);

        stopwatch.Stop();

        LogOutcome(requestName, response, stopwatch.ElapsedMilliseconds);

        return response;
    }

    private void LogOutcome(
        string requestName,
        TResponse response,
        long elapsedMilliseconds)
    {
        if (response is not Result result)
        {
            return;
        }

        if (result.IsSuccess)
        {
            _logger.LogInformation(
                "Completed request {RequestName} successfully in {ElapsedMs}ms",
                requestName,
                elapsedMilliseconds);
        }
        else
        {
            _logger.LogWarning(
                "Completed request {RequestName} with failure in {ElapsedMs}ms. Errors: {Errors}",
                requestName,
                elapsedMilliseconds,
                result.Errors.Select(e => e.Code).ToArray());
        }
    }
}
