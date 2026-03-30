using SpotifyClone.Billing.Domain.Aggregates.Subscriptions.ValueObjects;

namespace SpotifyClone.Billing.Domain.Aggregates.Subscriptions;

public interface ISubscriptionRepository
{
    Task AddAsync(
        Subscription subscription,
        CancellationToken cancellationToken = default);

    Task<Subscription?> GetByIdAsync(
            SubscriptionId id,
            CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Subscription subscription,
        CancellationToken cancellationToken = default);
}
