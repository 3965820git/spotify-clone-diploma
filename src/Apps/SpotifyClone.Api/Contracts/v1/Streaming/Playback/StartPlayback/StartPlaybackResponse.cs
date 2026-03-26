namespace SpotifyClone.Api.Contracts.v1.Streaming.Playback.StartPlayback;

public sealed record StartPlaybackResponse(
    string? HlsUrl,
    string? DashUrl,
    int? StartPositionMs,
    Guid? TrackId);
