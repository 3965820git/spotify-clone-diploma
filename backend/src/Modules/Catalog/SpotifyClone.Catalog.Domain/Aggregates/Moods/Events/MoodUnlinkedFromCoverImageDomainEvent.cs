using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Domain.Aggregates.Moods.Events;

public sealed record MoodUnlinkedFromCoverImageDomainEvent(
    ImageId ImageId)
    : DomainEvent;
