namespace SpotifyClone.Api.Contracts.v1.Catalog.Albums.RescheduleRelease;

public sealed record RescheduleAlbumReleaseRequest
{
    public required DateTimeOffset ReleaseDate { get; init; } // 2027-04-25T13:45:30
}
