using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.ValueObjects;

namespace SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.Events;

public sealed record AudioAssetCreatedDomainEvent(
    AudioAssetId AudioAssetId,
    TrackId TrackId,
    TimeSpan Duration)
    : DomainEvent;
