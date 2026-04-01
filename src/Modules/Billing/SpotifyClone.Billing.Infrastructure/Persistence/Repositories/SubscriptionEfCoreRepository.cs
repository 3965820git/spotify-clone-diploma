using Microsoft.EntityFrameworkCore;
using SpotifyClone.Billing.Domain.Aggregates.Subscriptions;
using SpotifyClone.Billing.Domain.Aggregates.Subscriptions.Enums;
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

    public async Task<Subscription?> GetActiveByUserIdAsync(
        UserId userId,
        CancellationToken cancellationToken = default)
        => await _subscriptions
        .Where(s => s.UserId == userId && s.Status == SubscriptionStatus.Active)
        .SingleOrDefaultAsync(cancellationToken);

    public async Task<bool> UserHasActiveSubscriptionAsync(
        UserId userId,
        CancellationToken cancellationToken = default)
        => await _subscriptions
        .AnyAsync(
            s => s.UserId == userId && s.Status == SubscriptionStatus.Active,
            cancellationToken);

    public async Task<Subscription?> GetByExternalIdAsync(
        string externalSubscriptionId,
        CancellationToken cancellationToken = default)
        => await _subscriptions
        .Where(s => s.ExternalIdentity.SubscriptionId == externalSubscriptionId)
        .SingleOrDefaultAsync(cancellationToken);

    public async Task<IEnumerable<Subscription>> GetActiveExpiredSubscriptionsAsync(
    DateTimeOffset now,
    CancellationToken cancellationToken)
        => await _subscriptions
        .Where(
            s => (s.Status == SubscriptionStatus.Active || s.Status == SubscriptionStatus.Canceled)
            && s.CurrentPeriodEnd < now)
        .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Subscription>> GetPastDueSubscriptionsAsync(
        CancellationToken cancellationToken = default)
        => await _subscriptions
        .Where(s => s.Status == SubscriptionStatus.Active && s.CurrentPeriodEnd < DateTimeOffset.UtcNow)
        .ToListAsync(cancellationToken);

    public async Task DeleteAsync(
        Subscription subscription,
        CancellationToken cancellationToken = default)
        => _subscriptions.Remove(subscription);
}
