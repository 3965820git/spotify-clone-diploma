namespace SpotifyClone.Api.Contracts.v1.Catalog.Tracks.UpdateFeaturedArtists;

public sealed record UpdateTrackFeaturedArtistsRequest
{
    public required IEnumerable<Guid> FeaturedArtistIds { get; init; }
}
