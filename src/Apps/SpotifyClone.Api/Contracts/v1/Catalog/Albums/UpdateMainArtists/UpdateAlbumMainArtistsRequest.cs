namespace SpotifyClone.Api.Contracts.v1.Catalog.Albums.UpdateMainArtists;

public sealed record UpdateAlbumMainArtistsRequest
{
    public required IEnumerable<Guid> MainArtistIds { get; init; }
}
