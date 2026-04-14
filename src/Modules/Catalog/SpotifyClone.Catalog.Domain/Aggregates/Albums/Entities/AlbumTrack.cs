using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Domain.Aggregates.Albums.Entities;

public sealed class AlbumTrack : Entity<TrackId, Guid>
{
    public int Position { get; private set; }

    internal AlbumTrack(TrackId trackId, int position)
        : base(trackId)
        => Position = position;

    internal void ChangePosition(int newPosition)
    {
        if (newPosition == Position)
        {
            return;
        }

        Position = newPosition;
    }

    private AlbumTrack()
    {
    }
}
