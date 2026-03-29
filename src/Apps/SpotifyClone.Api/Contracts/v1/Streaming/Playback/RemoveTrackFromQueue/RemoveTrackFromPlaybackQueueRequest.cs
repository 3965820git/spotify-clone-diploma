namespace SpotifyClone.Api.Contracts.v1.Streaming.Playback.RemoveTrackFromQueue;

public sealed record RemoveTrackFromPlaybackQueueRequest
{
    public required Guid TrackId { get; init; }
}
