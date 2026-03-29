using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.ValueObjects;

public sealed record PlaybackQueueId : StronglyTypedId<Guid>
{
    private PlaybackQueueId(Guid value)
        : base(value)
    {
    }

    public static PlaybackQueueId New()
        => new(Guid.NewGuid());

    public static PlaybackQueueId From(Guid value)
        => new(value);
}
