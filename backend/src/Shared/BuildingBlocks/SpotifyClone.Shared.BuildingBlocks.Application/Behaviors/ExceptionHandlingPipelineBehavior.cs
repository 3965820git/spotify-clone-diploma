using MediatR;
using Microsoft.Extensions.Logging;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Mappers;
using SpotifyClone.Shared.BuildingBlocks.Application.Behaviors.Helpers;
using SpotifyClone.Shared.BuildingBlocks.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Shared.BuildingBlocks.Application.Behaviors;

public sealed class ExceptionHandlingPipelineBehavior<TRequest, TResponse>(
    IEnumerable<IDomainExceptionMapper> mappers,
    ILogger<ExceptionHandlingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : notnull, IResult
{
    private readonly IEnumerable<IDomainExceptionMapper> _mappers = mappers;
    private readonly ILogger<ExceptionHandlingPipelineBehavior<TRequest, TResponse>> _logger = logger;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next(cancellationToken);
        }
        catch (Exception ex)
        {
            Error error;

            if (ex is DomainExceptionBase domainEx)
            {
                error = _mappers.Select(m => m.MapToError(domainEx))
                    .FirstOrDefault(e => e != CommonErrors.Unknown) ?? CommonErrors.Unknown;

                if (error == CommonErrors.Unknown)
                {
                    _logger.LogError(
                        domainEx,
                        "Unhandled domain exception while handling {RequestName}.",
                        typeof(TRequest).Name);
                }
                else
                {
                    _logger.LogWarning(
                        domainEx,
                        "Domain exception while handling {RequestName}: {Error}",
                        typeof(TRequest).Name,
                        error.Description);
                }
            }
            else
            {
                _logger.LogError(
                    ex,
                    "Unhandled exception while handling {RequestName}.",
                    typeof(TRequest).Name);

                error = CommonErrors.Internal;
            }

            return ResultFactory.CreateFailureResult<TResponse>(error);
        }
    }
}
