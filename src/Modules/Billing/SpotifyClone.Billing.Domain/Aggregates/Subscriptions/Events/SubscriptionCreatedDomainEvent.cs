using SpotifyClone.Billing.Domain.Aggregates.Subscriptions.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Billing.Domain.Aggregates.Subscriptions.Events;

public sealed record SubscriptionCreatedDomainEvent(
    SubscriptionId Id)
    : DomainEvent;
