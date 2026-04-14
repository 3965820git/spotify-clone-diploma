using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.UpdateMainArtists;

public sealed record UpdateAlbumMainArtistsCommand(
    Guid AlbumId,
    IEnumerable<Guid> MainArtists)
    : ICatalogPersistentCommand<UpdateAlbumMainArtistsCommandResult>;
