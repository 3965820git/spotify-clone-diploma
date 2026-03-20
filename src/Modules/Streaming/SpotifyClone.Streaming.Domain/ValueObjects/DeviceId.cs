using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Streaming.Domain.ValueObjects;

public sealed record DeviceId : StronglyTypedId<Guid>
{
    private DeviceId(Guid value)
        : base(value)
    {
    }

    public static DeviceId New()
        => new(Guid.NewGuid());

    public static DeviceId From(Guid value)
        => new(value);
}
