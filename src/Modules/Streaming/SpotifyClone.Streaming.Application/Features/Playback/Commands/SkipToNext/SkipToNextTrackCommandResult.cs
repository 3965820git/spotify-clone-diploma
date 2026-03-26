namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.SkipToNext;

public sealed record SkipToNextTrackCommandResult(
    bool IsQueueEmpty,
    string? HlsUrl = null,
    string? DashUrl = null,
    int? StartPositionMs = null,
    Guid? TrackId = null);
