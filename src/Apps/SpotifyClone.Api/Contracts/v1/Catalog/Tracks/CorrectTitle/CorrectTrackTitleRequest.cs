namespace SpotifyClone.Api.Contracts.v1.Catalog.Tracks.CorrectTitle;

public sealed record CorrectTrackTitleRequest
{
    public required string Title { get; init; }
}
