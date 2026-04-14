namespace SpotifyClone.Api.Contracts.v1.Streaming.Playback.SkipToTrack;

public sealed record SkipToTrackResponse(
    string? HlsUrl,
    string? DashUrl,
    int? StartPositionMs,
    Guid? TrackId);
