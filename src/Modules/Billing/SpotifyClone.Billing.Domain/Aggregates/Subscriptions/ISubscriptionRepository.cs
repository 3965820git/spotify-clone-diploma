using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Billing.Domain.Aggregates.Subscriptions;

public interface ISubscriptionRepository
{
    Task AddAsync(
        Subscription subscription,
        CancellationToken cancellationToken = default);

    Task<Subscription?> GetByUserIdAsync(
            UserId userId,
            CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Subscription subscription,
        CancellationToken cancellationToken = default);
}
