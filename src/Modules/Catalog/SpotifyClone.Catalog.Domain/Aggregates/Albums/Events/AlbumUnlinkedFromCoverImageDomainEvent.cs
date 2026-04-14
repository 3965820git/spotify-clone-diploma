using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Domain.Aggregates.Albums.Events;

public sealed record AlbumUnlinkedFromCoverImageDomainEvent(
    ImageId ImageId,
    IEnumerable<TrackId> Tracks)
    : DomainEvent;
