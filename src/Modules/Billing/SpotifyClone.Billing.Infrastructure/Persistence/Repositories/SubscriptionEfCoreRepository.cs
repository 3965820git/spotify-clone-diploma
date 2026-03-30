using Microsoft.EntityFrameworkCore;
using SpotifyClone.Billing.Domain.Aggregates.Subscriptions;
using SpotifyClone.Billing.Domain.Aggregates.Subscriptions.ValueObjects;
using SpotifyClone.Billing.Infrastructure.Persistence.Database;

namespace SpotifyClone.Billing.Infrastructure.Persistence.Repositories;

internal sealed class SubscriptionEfCoreRepository(
    BillingAppDbContext context)
    : ISubscriptionRepository
{
    private readonly DbSet<Subscription> _subscriptions = context.Subscriptions;

    public async Task AddAsync(
        Subscription subscription,
        CancellationToken cancellationToken = default)
        => await _subscriptions.AddAsync(subscription, cancellationToken);

    public async Task<Subscription?> GetByIdAsync(
        SubscriptionId id,
        CancellationToken cancellationToken = default)
        => await _subscriptions
        .Where(s => s.Id == id)
        .SingleOrDefaultAsync(cancellationToken);

    public async Task DeleteAsync(
        Subscription subscription,
        CancellationToken cancellationToken = default)
        => _subscriptions.Remove(subscription);
}
