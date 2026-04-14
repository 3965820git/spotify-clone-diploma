using SpotifyClone.Billing.Application.Abstractions.Repositories;
using SpotifyClone.Billing.Domain.Aggregates.Subscriptions;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions;

namespace SpotifyClone.Billing.Application.Abstractions;

public interface IBillingUnitOfWork : IUnitOfWork
{
    ISubscriptionRepository Subscriptions { get; }
    IOutboxRepository OutboxMessages { get; }
}
