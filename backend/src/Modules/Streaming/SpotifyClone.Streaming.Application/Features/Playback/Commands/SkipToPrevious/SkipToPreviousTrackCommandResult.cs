namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.SkipToPrevious;

public sealed record SkipToPreviousTrackCommandResult(
    bool IsPreviousQueueEmpty,
    string? HlsUrl = null,
    string? DashUrl = null,
    int? StartPositionMs = null,
    Guid? TrackId = null);
