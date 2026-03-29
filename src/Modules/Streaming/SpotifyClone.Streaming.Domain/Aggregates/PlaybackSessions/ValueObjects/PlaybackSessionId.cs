using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.ValueObjects;

public sealed record PlaybackSessionId : StronglyTypedId<Guid>
{
    private PlaybackSessionId(Guid value)
        : base(value)
    {
    }

    public static PlaybackSessionId New()
        => new(Guid.NewGuid());

    public static PlaybackSessionId From(Guid value)
        => new(value);
}
