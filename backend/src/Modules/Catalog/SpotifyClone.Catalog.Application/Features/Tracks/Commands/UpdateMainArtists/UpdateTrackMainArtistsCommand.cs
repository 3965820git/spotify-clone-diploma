using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.UpdateMainArtists;

public sealed record UpdateTrackMainArtistsCommand(
    Guid TrackId,
    IEnumerable<Guid> MainArtists)
    : ICatalogPersistentCommand<UpdateTrackMainArtistsCommandResult>;
