namespace SpotifyClone.Api.Contracts.v1.Catalog.Albums.Create;

public sealed record CreateAlbumRequest
{
    public required string Title { get; init; }
    public required IEnumerable<Guid> MainArtistIds { get; init; }
}
