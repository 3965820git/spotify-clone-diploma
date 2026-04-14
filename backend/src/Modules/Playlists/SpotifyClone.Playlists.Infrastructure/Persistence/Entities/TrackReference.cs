using SpotifyClone.Playlists.Infrastructure.Persistence.Entities.Enums;

namespace SpotifyClone.Playlists.Infrastructure.Persistence.Entities;

public sealed class TrackReference
{
    public Guid Id { get; set; }
    public TrackReferenceStatus Status { get; set; } = null!;
    public Guid? CoverImageId { get; set; }

    public TrackReference(Guid id, TrackReferenceStatus status, Guid? coverImageId)
    {
        Id = id;
        Status = status;
        CoverImageId = coverImageId;
    }

    private TrackReference()
    {
    }
}
