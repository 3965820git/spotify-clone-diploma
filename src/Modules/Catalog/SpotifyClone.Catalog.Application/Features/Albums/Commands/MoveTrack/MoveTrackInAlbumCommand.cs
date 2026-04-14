using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.MoveTrack;

public sealed record MoveTrackInAlbumCommand(
    Guid AlbumId,
    Guid TrackId,
    int TargetPositionIndex)
    : ICatalogPersistentCommand<MoveTrackInAlbumCommandResult>;
