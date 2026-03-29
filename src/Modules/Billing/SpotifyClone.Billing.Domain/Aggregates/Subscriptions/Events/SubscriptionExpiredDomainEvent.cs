using SpotifyClone.Billing.Domain.Aggregates.Subscriptions.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Billing.Domain.Aggregates.Subscriptions.Events;

public sealed record SubscriptionExpiredDomainEvent(
    SubscriptionId Id,
    UserId UserId)
    : DomainEvent;
