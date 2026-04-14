using Microsoft.EntityFrameworkCore;
using SpotifyClone.Billing.Application.Abstractions.Data;
using SpotifyClone.Billing.Application.Features.Subscriptions.Queries;
using SpotifyClone.Billing.Domain.Aggregates.Subscriptions.Enums;
using SpotifyClone.Billing.Infrastructure.Persistence.Database;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Billing.Infrastructure.Persistence.Queries;

internal sealed class SubscriptionEfCoreReadService(
    BillingAppDbContext context)
    : ISubscriptionReadService
{
    private readonly BillingAppDbContext _context = context;

    public async Task<SubscriptionDetails> GetDetailsByUserIdAsync(
        UserId userId,
        CancellationToken cancellationToken = default)
        => await _context.Subscriptions
        .AsNoTracking()
        .OrderByDescending(s => s.Status == SubscriptionStatus.Active)
        .ThenByDescending(s => s.CurrentPeriodEnd)
        .Where(s => s.UserId == userId)
        .Select(s => new SubscriptionDetails(
            s.Status == SubscriptionStatus.Active || s.Status == SubscriptionStatus.Canceled,
            s.Status.ToString().ToLowerInvariant(),
            s.CurrentPeriodEnd,
            s.CancelAtPeriodEnd))
        .FirstOrDefaultAsync(cancellationToken)
        ?? new SubscriptionDetails(false, null, null, null);
}
