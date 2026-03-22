namespace SpotifyClone.Streaming.Application.Features.Playback.Queries;

public sealed record PlaybackSessionDetails(
    Guid Id,
    Guid UserId,
    Guid TrackId,
    Guid DeviceId,
    string ContextType,
    Guid? ContextExternalId,
    int CurrentPositionMs,
    bool IsPlaying,
    bool Shuffle,
    string RepeatMode,
    DateTimeOffset UpdatedAtUtc);
