namespace SpotifyClone.Api.Contracts.v1.Playlists.MoveTrack;

public sealed record MoveTrackInPlaylistRequest
{
    public required int TargetPositionIndex { get; init; }
}
