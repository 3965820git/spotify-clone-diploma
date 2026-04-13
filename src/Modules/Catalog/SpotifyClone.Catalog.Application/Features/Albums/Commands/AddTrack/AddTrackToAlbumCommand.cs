using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.AddTrack;

public sealed record AddTrackToAlbumCommand(
    Guid AlbumId,
    Guid TrackId)
    : ICatalogPersistentCommand<AddTrackToAlbumCommandResult>;
