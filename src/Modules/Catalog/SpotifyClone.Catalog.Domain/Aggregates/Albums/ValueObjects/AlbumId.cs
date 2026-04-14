using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Catalog.Domain.Aggregates.Albums.ValueObjects;

public sealed record AlbumId : StronglyTypedId<Guid>
{
    private AlbumId(Guid value)
        : base(value)
    {
    }

    public static AlbumId New()
        => new(Guid.NewGuid());

    public static AlbumId From(Guid value)
        => new(value);
}
