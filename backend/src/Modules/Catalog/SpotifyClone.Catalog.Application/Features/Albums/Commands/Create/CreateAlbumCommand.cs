using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.Create;

public sealed record CreateAlbumCommand(
    string Title,
    IEnumerable<Guid> MainArtists)
    : ICatalogPersistentCommand<CreateAlbumCommandResult>;
