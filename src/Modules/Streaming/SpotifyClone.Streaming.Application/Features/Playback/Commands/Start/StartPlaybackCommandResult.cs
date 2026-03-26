namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.Start;

public sealed record StartPlaybackCommandResult(
    string? HlsUrl = null,
    string? DashUrl = null,
    int? StartPositionMs = null,
    Guid? TrackId = null);
