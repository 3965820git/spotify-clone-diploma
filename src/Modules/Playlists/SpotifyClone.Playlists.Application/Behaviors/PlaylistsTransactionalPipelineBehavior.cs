using Microsoft.Extensions.Logging;
using SpotifyClone.Playlists.Application.Abstractions;
using SpotifyClone.Shared.BuildingBlocks.Application.Behaviors;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Playlists.Application.Behaviors;

public sealed class PlaylistsTransactionalPipelineBehavior<TRequest, TResponse>(
    IPlaylistsUnitOfWork unit,
    ILogger<PlaylistsTransactionalPipelineBehavior<TRequest, TResponse>> logger)
    : TransactionalPipelineBehaviorBase<TRequest, TResponse>(
        unit, typeof(IPlaylistsPersistentCommand), typeof(IPlaylistsPersistentCommand<>), logger)
    where TRequest : notnull
    where TResponse : IResult;
