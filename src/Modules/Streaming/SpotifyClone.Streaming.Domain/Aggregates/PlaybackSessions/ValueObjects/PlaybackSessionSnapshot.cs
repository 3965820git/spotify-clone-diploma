namespace SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.ValueObjects;

public sealed record PlaybackSessionSnapshot(
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
    DateTimeOffset UpdatedAtUtc,
    IEnumerable<Guid> CurrentQueue,
    IEnumerable<Guid> PreviousQueue,
    IEnumerable<Guid> OriginalQueue);
