namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.Start;

public sealed record StartPlaybackCommandResult(
    string HlsUrl,
    string DashUrl,
    int StartPositionMs,
    Guid TrackId);
