namespace SpotifyClone.Api.Contracts.v1.Streaming.Player.UpdatePosition;

public sealed record UpdatePlaybackPositionRequest
{
    public required Guid DeviceId { get; init; }
    public required int PositionMs { get; init; }
}
