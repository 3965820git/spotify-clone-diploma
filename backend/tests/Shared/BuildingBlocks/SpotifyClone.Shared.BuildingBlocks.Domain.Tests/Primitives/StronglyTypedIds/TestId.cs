using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Shared.BuildingBlocks.Domain.Tests.Primitives.StronglyTypedIds;

internal sealed record TestId : StronglyTypedId<Guid>
{
    private TestId(Guid value)
        : base(value)
    {
    }

    public static TestId New()
        => new TestId(Guid.NewGuid());

    public static TestId From(Guid value)
        => new TestId(value);
}
