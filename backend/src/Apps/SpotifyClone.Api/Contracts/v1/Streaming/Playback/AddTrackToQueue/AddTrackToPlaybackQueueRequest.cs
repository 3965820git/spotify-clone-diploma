namespace SpotifyClone.Api.Contracts.v1.Streaming.Playback.AddTrackToQueue;

public sealed record AddTrackToPlaybackQueueRequest
{
    public required Guid TrackId { get; init; }
}
