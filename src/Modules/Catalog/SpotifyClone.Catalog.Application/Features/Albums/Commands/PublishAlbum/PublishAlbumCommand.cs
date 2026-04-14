using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.PublishAlbum;

public sealed record PublishAlbumCommand(
    Guid AlbumId,
    DateTimeOffset ReleaseDate)
    : ICatalogPersistentCommand<PublishAlbumCommandResult>;
