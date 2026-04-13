using Microsoft.Extensions.Logging;
using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Shared.BuildingBlocks.Application.Behaviors;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Catalog.Application.Behaviors;

public sealed class CatalogTransactionalPipelineBehavior<TRequest, TResponse>(
    ICatalogUnitOfWork unit,
    ILogger<CatalogTransactionalPipelineBehavior<TRequest, TResponse>> logger)
    : TransactionalPipelineBehaviorBase<TRequest, TResponse>(
        unit, typeof(ICatalogPersistentCommand), typeof(ICatalogPersistentCommand<>), logger)
    where TRequest : notnull
    where TResponse : IResult;
