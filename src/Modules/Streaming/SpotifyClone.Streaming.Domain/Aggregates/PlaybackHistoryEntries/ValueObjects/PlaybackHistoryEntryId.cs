using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Streaming.Domain.Aggregates.PlaybackHistoryEntries.ValueObjects;

public sealed record PlaybackHistoryEntryId : StronglyTypedId<Guid>
{
    private PlaybackHistoryEntryId(Guid value)
        : base(value)
    {
    }

    public static PlaybackHistoryEntryId New()
        => new(Guid.NewGuid());

    public static PlaybackHistoryEntryId From(Guid value)
        => new(value);
}
