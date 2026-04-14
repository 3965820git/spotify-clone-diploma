namespace SpotifyClone.Api.Contracts.v1.Catalog.Albums.PublishAlbum;

public sealed record PublishAlbumRequest
{
    public required DateTimeOffset ReleaseDate { get; init; } // 2026-04-25T13:45:30
}
