using SpotifyClone.Billing.Application.Features.Subscriptions.Queries;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Billing.Application.Abstractions.Data;

public interface ISubscriptionReadService
{
    Task<SubscriptionDetails> GetDetailsByUserIdAsync(
        UserId userId,
        CancellationToken cancellationToken = default);
}
