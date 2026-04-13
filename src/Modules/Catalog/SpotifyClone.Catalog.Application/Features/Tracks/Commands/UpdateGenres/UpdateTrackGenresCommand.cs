using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.UpdateGenres;

public sealed record UpdateTrackGenresCommand(
    Guid TrackId,
    IEnumerable<Guid> Genres)
    : ICatalogPersistentCommand<UpdateTrackGenresCommandResult>;
