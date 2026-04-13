using SpotifyClone.Billing.Domain.Aggregates.Subscriptions.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Infrastructure.Persistence.Converters;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Billing.Infrastructure.Persistence.Configurations.Converters;

internal static class BillingEfCoreValueConverters
{
    public static readonly StronglyTypedIdEfCoreConverter<SubscriptionId, Guid> SubscriptionIdConverter = new(
        v => SubscriptionId.From(v));

    public static readonly StronglyTypedIdEfCoreConverter<UserId, Guid> UserIdConverter = new(
        v => UserId.From(v));
}
