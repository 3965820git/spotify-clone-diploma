using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.UnpublishAlbum;

public sealed record UnpublishAlbumCommand(
    Guid AlbumId)
    : ICatalogPersistentCommand<UnpublishAlbumCommandResult>;
