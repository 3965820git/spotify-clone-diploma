using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Domain.Aggregates.Genres.Events;

public sealed record GenreUnlinkedFromCoverImageDomainEvent(
    ImageId ImageId)
    : DomainEvent;
