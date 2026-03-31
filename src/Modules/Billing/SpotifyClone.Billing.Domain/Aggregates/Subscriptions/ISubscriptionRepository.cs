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

    Task<Subscription?> GetByExternalIdAsync(
        string externalSubscriptionId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Subscription>> GetActiveExpiredSubscriptionsAsync(
        DateTimeOffset now,
        CancellationToken cancellationToken);

    Task<IEnumerable<Subscription>> GetPastDueSubscriptionsAsync(
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Subscription subscription,
        CancellationToken cancellationToken = default);
}
