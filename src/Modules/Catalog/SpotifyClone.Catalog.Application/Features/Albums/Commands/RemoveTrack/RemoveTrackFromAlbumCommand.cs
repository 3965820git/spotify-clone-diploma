using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.RemoveTrack;

public sealed record RemoveTrackFromAlbumCommand(
    Guid AlbumId,
    Guid TrackId)
    : ICatalogPersistentCommand<RemoveTrackFromAlbumCommandResult>;
