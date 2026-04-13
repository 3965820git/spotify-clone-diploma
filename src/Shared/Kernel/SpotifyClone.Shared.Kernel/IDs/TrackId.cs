using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Shared.Kernel.IDs;

public sealed record TrackId : StronglyTypedId<Guid>
{
    private TrackId(Guid value)
        : base(value)
    {
    }

    public static TrackId New()
        => new(Guid.NewGuid());

    public static TrackId From(Guid value)
        => new(value);
}
