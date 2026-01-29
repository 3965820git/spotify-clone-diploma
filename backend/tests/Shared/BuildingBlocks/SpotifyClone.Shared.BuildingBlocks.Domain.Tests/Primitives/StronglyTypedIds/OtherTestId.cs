using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Shared.BuildingBlocks.Domain.Tests.Primitives.StronglyTypedIds;

internal sealed record OtherTestId : StronglyTypedId<Guid>
{
    private OtherTestId(Guid value)
        : base(value)
    {
    }

    public static OtherTestId New()
        => new OtherTestId(Guid.NewGuid());

    public static OtherTestId From(Guid value)
        => new OtherTestId(value);
}
