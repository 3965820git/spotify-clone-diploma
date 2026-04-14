using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Playlists.Domain.Aggregates.Playlists.ValueObjects;

public sealed record PlaylistId : StronglyTypedId<Guid>
{
    private PlaylistId(Guid value)
        : base(value)
    {
    }

    public static PlaylistId New()
        => new(Guid.NewGuid());

    public static PlaylistId From(Guid value)
        => new(value);
}
