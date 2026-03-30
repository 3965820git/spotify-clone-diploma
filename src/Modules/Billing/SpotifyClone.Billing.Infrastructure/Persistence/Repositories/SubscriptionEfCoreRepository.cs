using Microsoft.EntityFrameworkCore;
using SpotifyClone.Billing.Domain.Aggregates.Subscriptions;
using SpotifyClone.Billing.Infrastructure.Persistence.Database;
using SpotifyClone.Shared.Kernel.IDs;

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

    public async Task<Subscription?> GetByUserIdAsync(
        UserId userId,
        CancellationToken cancellationToken = default)
        => await _subscriptions
        .Where(s => s.UserId == userId)
        .SingleOrDefaultAsync(cancellationToken);

    public async Task DeleteAsync(
        Subscription subscription,
        CancellationToken cancellationToken = default)
        => _subscriptions.Remove(subscription);
}
