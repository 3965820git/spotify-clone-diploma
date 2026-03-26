namespace SpotifyClone.Api.Contracts.v1.Streaming.Playback.SkipToNext;

public sealed record SkipToNextTrackResponse(
    string? HlsUrl,
    string? DashUrl,
    int? StartPositionMs,
    Guid? TrackId);
