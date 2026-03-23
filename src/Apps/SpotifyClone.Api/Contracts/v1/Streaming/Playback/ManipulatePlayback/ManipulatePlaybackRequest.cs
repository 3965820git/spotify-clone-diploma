namespace SpotifyClone.Api.Contracts.v1.Streaming.Playback.ManipulatePlayback;

public sealed record ManipulatePlaybackRequest
{
    public required Guid DeviceId { get; init; }
}
