using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Catalog.Domain.Aggregates.Tracks.ValueObjects;

public sealed record AudioFileId : StronglyTypedId<Guid>
{
    private AudioFileId(Guid value)
        : base(value)
    {
    }

    public static AudioFileId New()
        => new(Guid.NewGuid());

    public static AudioFileId From(Guid value)
        => new(value);
}
