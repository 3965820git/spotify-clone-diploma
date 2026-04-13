using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Playlists.Domain.Aggregates.Playlists.Events;

public sealed record PlaylistUnlinkedFromCoverImageDomainEvent(
    ImageId ImageId)
    : DomainEvent;
