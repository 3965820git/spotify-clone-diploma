using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.ValueObjects;

public sealed record AudioAssetId : StronglyTypedId<Guid>
{
    private AudioAssetId(Guid value)
        : base(value)
    {
    }

    public static AudioAssetId New()
        => new(Guid.NewGuid());

    public static AudioAssetId From(Guid value)
        => new(value);

}
