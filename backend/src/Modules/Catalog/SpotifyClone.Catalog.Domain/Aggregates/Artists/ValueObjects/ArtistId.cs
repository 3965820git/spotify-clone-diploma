using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Catalog.Domain.Aggregates.Artists.ValueObjects;

public sealed record ArtistId : StronglyTypedId<Guid>
{
    private ArtistId(Guid value) : base(value)
    {
    }

    public static ArtistId New()
        => new(Guid.NewGuid());

    public static ArtistId From(Guid value)
        => new(value);
}
