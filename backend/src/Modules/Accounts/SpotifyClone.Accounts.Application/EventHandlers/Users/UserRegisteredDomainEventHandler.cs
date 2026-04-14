using MediatR;
using SpotifyClone.Accounts.Application.Abstractions;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Accounts.Domain.Aggregates.Users.Events;
using SpotifyClone.Shared.BuildingBlocks.Application.Outbox;
using SpotifyClone.Shared.IntegrationEvents.Accounts.Users;

namespace SpotifyClone.Accounts.Application.EventHandlers.Users;

internal sealed class UserRegisteredDomainEventHandler(
    IAccountsUnitOfWork unit,
    IAccountVerificationService verificationService)
    : INotificationHandler<UserRegisteredDomainEvent>
{
    private readonly IAccountsUnitOfWork _unit = unit;
    private readonly IAccountVerificationService _verificationService = verificationService;

    public async Task Handle(
        UserRegisteredDomainEvent notification,
        CancellationToken cancellationToken)
    {
        await _verificationService.SendVerificationEmailAsync(
            notification.Email,
            notification.UserId.Value,
            notification.ConfirmationToken,
            cancellationToken);

        var integrationEvent = new UserRegisteredIntegrationEvent(
                notification.UserId.Value,
                notification.DisplayName,
                notification.CoverImageId?.Value);

        var message = OutboxMessage.FromIntegrationEvent(integrationEvent);

        await _unit.OutboxMessages.AddAsync(message, cancellationToken);
        await _unit.CommitAsync(cancellationToken);
    }
}
