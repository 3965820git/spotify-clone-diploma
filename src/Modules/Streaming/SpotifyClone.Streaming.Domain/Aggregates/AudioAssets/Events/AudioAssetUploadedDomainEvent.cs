using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.Events;

public sealed record AudioAssetUploadedDomainEvent(
    TrackId TrackId)
    : DomainEvent;
