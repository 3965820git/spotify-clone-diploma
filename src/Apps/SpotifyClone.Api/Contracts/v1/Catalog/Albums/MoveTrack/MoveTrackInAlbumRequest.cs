namespace SpotifyClone.Api.Contracts.v1.Catalog.Albums.MoveTrack;

public sealed record MoveTrackInAlbumRequest
{
    public required int TargetPositionIndex { get; init; }
}
