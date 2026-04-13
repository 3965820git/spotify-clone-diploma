using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Catalog.Domain.Aggregates.Genres.ValueObjects;

public sealed record GenreId : StronglyTypedId<Guid>
{
    private GenreId(Guid value)
        : base(value)
    {
    }

    public static GenreId New()
        => new(Guid.NewGuid());

    public static GenreId From(Guid value)
        => new(value);
}
