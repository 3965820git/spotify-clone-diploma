using MediatR;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Shared.IntegrationEvents.Billing.Subscriptions;

namespace SpotifyClone.Accounts.Application.EventHandlers.Users;

internal sealed class UserSubscriptionStatusChangedIntegrationEventHandler(
    IIdentityService identity)
    : INotificationHandler<UserSubscriptionStatusChangedIntegrationEvent>
{
    private readonly IIdentityService _identity = identity;

    public async Task Handle(
        UserSubscriptionStatusChangedIntegrationEvent notification,
        CancellationToken cancellationToken)
        => await _identity.UpdateUserSubscriptionLevelAsync(
            notification.UserId,
            notification.IsPremium ? "premium" : "free",
            cancellationToken);
}
