using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Shared.Kernel.IDs;

public sealed record UserId : StronglyTypedId<Guid>
{
    private UserId(Guid value)
        : base(value)
    {
    }

    public static UserId New()
        => new(Guid.NewGuid());

    public static UserId From(Guid value)
        => new(value);
}
