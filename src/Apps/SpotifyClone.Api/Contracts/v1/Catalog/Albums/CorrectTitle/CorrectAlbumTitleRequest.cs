namespace SpotifyClone.Api.Contracts.v1.Catalog.Albums.CorrectTitle;

public sealed record CorrectAlbumTitleRequest
{
    public required string Title { get; init; }
}
