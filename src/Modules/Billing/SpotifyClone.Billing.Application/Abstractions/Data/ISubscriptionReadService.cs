using SpotifyClone.Billing.Application.Features.Subscriptions.Queries;
using SpotifyClone.Billing.Domain.Aggregates.Subscriptions.ValueObjects;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Billing.Application.Abstractions.Data;

public interface ISubscriptionReadService
{
    Task<SubscriptionDetails?> GetDetailsAsync(
        SubscriptionId id,
        CancellationToken cancellationToken = default);

    Task<bool> UserHasActiveSubscriptionAsync(
        UserId userId,
        CancellationToken cancellationToken = default);
}
