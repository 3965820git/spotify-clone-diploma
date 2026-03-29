namespace SpotifyClone.Streaming.Infrastructure.Persistence.Models;

public sealed record PlaybackSessionData(
    Guid Id,
    Guid UserId,
    Guid? TrackId,
    Guid DeviceId,
    string ContextType,
    Guid? ContextExternalId,
    int CurrentPositionMs,
    bool IsPlaying,
    bool IsShuffled,
    int RepeatMode,
    DateTimeOffset UpdatedAtUtc);
