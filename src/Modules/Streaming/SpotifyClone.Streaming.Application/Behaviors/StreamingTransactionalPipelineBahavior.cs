using Microsoft.Extensions.Logging;
using SpotifyClone.Shared.BuildingBlocks.Application.Behaviors;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Streaming.Application.Abstractions;

namespace SpotifyClone.Streaming.Application.Behaviors;

public sealed class StreamingTransactionalPipelineBehavior<TRequest, TResponse>(
    IStreamingUnitOfWork unit,
    ILogger<StreamingTransactionalPipelineBehavior<TRequest, TResponse>> logger)
    : TransactionalPipelineBehaviorBase<TRequest, TResponse>(
        unit, typeof(IStreamingPersistentCommand), typeof(IStreamingPersistentCommand<>), logger)
    where TRequest : notnull
    where TResponse : IResult;
