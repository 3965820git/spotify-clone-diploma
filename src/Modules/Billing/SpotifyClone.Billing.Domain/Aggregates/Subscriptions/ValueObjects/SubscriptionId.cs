using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Billing.Domain.Aggregates.Subscriptions.ValueObjects;

public sealed record SubscriptionId : StronglyTypedId<Guid>
{
    private SubscriptionId(Guid value)
        : base(value)
    {
    }

    public static SubscriptionId New()
        => new(Guid.NewGuid());

    public static SubscriptionId From(Guid value)
        => new(value);
}
