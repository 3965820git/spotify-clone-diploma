using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Billing.Domain.Aggregates.Subscriptions.Events;

public sealed record SubscriptionActivatedDomainEvent(
    UserId UserId)
    : DomainEvent;
