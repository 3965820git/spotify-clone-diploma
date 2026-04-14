using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.UpdateFeaturedArtists;

public sealed record UpdateTrackFeaturedArtistsCommand(
    Guid TrackId,
    IEnumerable<Guid> FeaturedArtists)
    : ICatalogPersistentCommand<UpdateTrackFeaturedArtistsCommandResult>;
