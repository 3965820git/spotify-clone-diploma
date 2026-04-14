using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.Delete;

public sealed record DeleteAlbumCommand(
    Guid AlbumId)
    : ICatalogPersistentCommand<DeleteAlbumCommandResult>;
