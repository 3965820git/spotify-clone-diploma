using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackHistoryEntries.Exceptions;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackHistoryEntries.ValueObjects;
using SpotifyClone.Streaming.Domain.ValueObjects;

namespace SpotifyClone.Streaming.Domain.Aggregates.PlaybackHistoryEntries;

public sealed class PlaybackHistoryEntry
    : AggregateRoot<PlaybackHistoryEntryId, Guid>
{
    public UserId UserId { get; private set; } = null!;
    public TrackId TrackId { get; private set; } = null!;
    public PlaybackContext Context { get; private set; } = null!;
    public DateTimeOffset PlayedAtUtc { get; private set; }

    public static PlaybackHistoryEntry Create(
        PlaybackHistoryEntryId id,
        UserId userId,
        TrackId trackId,
        PlaybackContext context,
        DateTimeOffset playedAtUtc)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(userId);
        ArgumentNullException.ThrowIfNull(trackId);
        ArgumentNullException.ThrowIfNull(context);

        if (playedAtUtc > DateTimeOffset.UtcNow)
        {
            throw new InvalidPlayedAtDateDomainException("Played at date cannot be in the future");
        }

        return new PlaybackHistoryEntry(id, userId, trackId, context, playedAtUtc);
    }

    private PlaybackHistoryEntry(
        PlaybackHistoryEntryId id,
        UserId userId,
        TrackId trackId,
        PlaybackContext context,
        DateTimeOffset playedAtUtc)
        : base(id)
    {
        UserId = userId;
        TrackId = trackId;
        Context = context;
        PlayedAtUtc = playedAtUtc;
    }

    private PlaybackHistoryEntry()
    {
    }
}
