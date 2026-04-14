namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.SkipToNext;

public sealed record SkipToNextTrackCommandResult(
    bool IsCurrentQueueEmpty,
    string? HlsUrl = null,
    string? DashUrl = null,
    int? StartPositionMs = null,
    Guid? TrackId = null);
