using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.RescheduleRelease;

public sealed record RescheduleAlbumReleaseCommand(
    Guid AlbumId,
    DateTimeOffset ReleaseDate)
    : ICatalogPersistentCommand<RescheduleAlbumReleaseCommandResult>;
