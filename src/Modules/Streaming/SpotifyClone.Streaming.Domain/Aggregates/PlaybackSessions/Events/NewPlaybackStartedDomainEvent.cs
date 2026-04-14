using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Domain.ValueObjects;

namespace SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.Events;

public sealed record NewPlaybackStartedDomainEvent(
    UserId UserId,
    TrackId TrackId,
    PlaybackContext Context,
    DateTimeOffset StartedAtUtc)
    : DomainEvent;
