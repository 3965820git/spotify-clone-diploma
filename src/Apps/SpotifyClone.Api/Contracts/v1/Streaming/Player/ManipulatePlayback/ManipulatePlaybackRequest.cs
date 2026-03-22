namespace SpotifyClone.Api.Contracts.v1.Streaming.Player.ManipulatePlayback;

public sealed record ManipulatePlaybackRequest
{
    public required Guid DeviceId { get; init; }
}
