using MediatR;
using SpotifyClone.Billing.Application.Abstractions;
using SpotifyClone.Billing.Application.Abstractions.Repositories;
using SpotifyClone.Billing.Domain.Aggregates.Subscriptions;
using SpotifyClone.Billing.Infrastructure.Persistence.Database;
using SpotifyClone.Shared.BuildingBlocks.Infrastructure.Persistence;

namespace SpotifyClone.Billing.Infrastructure.Persistence;

internal sealed class BillingEfCoreUnitOfWork(
    BillingAppDbContext context,
    ISubscriptionRepository subscriptions,
    IOutboxRepository outbox,
    IPublisher publisher)
    : EfCoreUnitOfWorkBase<BillingAppDbContext>(context, publisher),
    IBillingUnitOfWork
{
    public ISubscriptionRepository Subscriptions => subscriptions;
    public IOutboxRepository OutboxMessages => outbox;
}
