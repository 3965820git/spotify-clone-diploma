using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Playlists.Domain.Aggregates.Playlists.Events;

public sealed record PlaylistLinkedToCoverImageDomainEvent(
    ImageId ImageId)
    : DomainEvent;
