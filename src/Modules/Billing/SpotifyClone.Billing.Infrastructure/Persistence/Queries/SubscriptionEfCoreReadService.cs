using Microsoft.EntityFrameworkCore;
using SpotifyClone.Billing.Application.Abstractions.Data;
using SpotifyClone.Billing.Application.Features.Subscriptions.Queries;
using SpotifyClone.Billing.Domain.Aggregates.Subscriptions.ValueObjects;
using SpotifyClone.Billing.Infrastructure.Persistence.Database;

namespace SpotifyClone.Billing.Infrastructure.Persistence.Queries;

internal sealed class SubscriptionEfCoreReadService(
    BillingAppDbContext context) : ISubscriptionReadService
{
    private readonly BillingAppDbContext _context = context;

    public async Task<SubscriptionDetails?> GetDetailsAsync(
        SubscriptionId id,
        CancellationToken cancellationToken = default)
        => await _context.Subscriptions
        .AsNoTracking()
        .Where(s => s.Id == id)
        .Select(s => new SubscriptionDetails(
            s.Id.Value,
            s.UserId.Value,
            s.ExternalIdentity.CustomerId,
            s.ExternalIdentity.SubscriptionId,
            s.Status.ToString(),
            s.CurrentPeriodStart,
            s.CurrentPeriodEnd,
            s.CancelAtPeriodEnd))
        .SingleOrDefaultAsync(cancellationToken);
}
