using MediatR;
using Microsoft.Extensions.Logging;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Shared.BuildingBlocks.Application.Behaviors;

public abstract class TransactionalPipelineBehaviorBase<TRequest, TResponse>(
    IUnitOfWork unit,
    Type nonGenericPersistentCommandType,
    Type genericPersistentCommandType,
    ILogger<TransactionalPipelineBehaviorBase<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : IResult
{
    private readonly IUnitOfWork _unit = unit;
    private readonly Type _nonGenericPersistentCommandInterface = nonGenericPersistentCommandType;
    private readonly Type _genericPersistentCommandType = genericPersistentCommandType;
    private readonly ILogger<TransactionalPipelineBehaviorBase<TRequest, TResponse>> _logger = logger;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_nonGenericPersistentCommandInterface.IsAssignableFrom(typeof(TRequest)) &&
            !ImplementsGenericInterface(typeof(TRequest), _genericPersistentCommandType))
        {
            return await next(cancellationToken);
        }

        _logger.LogInformation("Beginning transaction for {RequestType}", typeof(TRequest).Name);

        TResponse response = await next(cancellationToken);

        if (response.IsFailure)
        {
            return response;
        }

        await _unit.CommitAsync(cancellationToken);

        _logger.LogInformation("Committed transaction for {RequestType}", typeof(TRequest).Name);

        return response;
    }

    private static bool ImplementsGenericInterface(Type type, Type genericInterface)
        => type.GetInterfaces()
        .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericInterface);
}
