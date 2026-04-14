using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Catalog.Domain.Aggregates.Moods.ValueObjects;

public sealed record MoodId : StronglyTypedId<Guid>
{
    private MoodId(Guid value)
        : base(value)
    {
    }

    public static MoodId New()
        => new(Guid.NewGuid());

    public static MoodId From(Guid value)
        => new(value);
}
