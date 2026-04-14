namespace SpotifyClone.Api.Contracts.v1.Catalog.Albums.AddTrackToAlbum;

public sealed record AddTrackToAlbumRequest
{
    public required Guid TrackId { get; init; }
}
