namespace SpotifyClone.Streaming.Application.Features.Playback.Queries;

public sealed record PlaybackHistoryEntryDetails(
    Guid Id,
    Guid UserId,
    Guid TrackId,
    string ContextType,
    Guid? ContextExternalId,
    DateTimeOffset PlayedAtUtc);
