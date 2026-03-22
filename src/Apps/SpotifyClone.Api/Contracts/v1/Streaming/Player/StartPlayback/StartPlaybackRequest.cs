namespace SpotifyClone.Api.Contracts.v1.Streaming.Player.StartPlayback;

public sealed record StartPlaybackRequest
{
    public required Guid TrackId { get; init; }
    public required Guid DeviceId { get; init; }
    public required string ContextType { get; init; }
    public required Guid? ContextExternalId { get; init; }
    public required int? PositionMs { get; init; }
}
