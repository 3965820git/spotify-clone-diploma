using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.Create;

public sealed record CreateTrackCommand(
    string Title,
    bool ContainsExplicitContent,
    Guid AlbumId,
    IEnumerable<Guid> MainArtists,
    IEnumerable<Guid> FeaturedArtists,
    IEnumerable<Guid> Genres,
    IEnumerable<Guid> Moods)
    : ICatalogPersistentCommand<CreateTrackCommandResult>;
