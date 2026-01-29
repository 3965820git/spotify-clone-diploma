using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Shared.Kernel.IDs;

public sealed record ImageId : StronglyTypedId<Guid>
{
    private ImageId(Guid value)
        : base(value)
    {
    }

    public static ImageId New()
        => new(Guid.NewGuid());

    public static ImageId From(Guid value)
        => new(value);
}
