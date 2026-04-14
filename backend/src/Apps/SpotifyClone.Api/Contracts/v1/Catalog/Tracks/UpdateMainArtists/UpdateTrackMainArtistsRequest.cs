namespace SpotifyClone.Api.Contracts.v1.Catalog.Tracks.UpdateMainArtists;

public sealed record UpdateTrackMainArtistsRequest
{
    public required IEnumerable<Guid> MainArtistIds { get; init; }
}
