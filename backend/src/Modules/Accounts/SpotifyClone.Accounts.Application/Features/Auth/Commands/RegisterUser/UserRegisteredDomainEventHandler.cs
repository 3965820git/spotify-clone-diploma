using MediatR;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Accounts.Domain.Aggregates.Users.Events;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.RegisterUser;

internal sealed class UserRegisteredDomainEventHandler(
    IAccountVerificationService emailVerificationService)
    : INotificationHandler<UserRegisteredDomainEvent>
{
    private readonly IAccountVerificationService _emailVerificationService = emailVerificationService;

    public async Task Handle(
        UserRegisteredDomainEvent notification,
        CancellationToken cancellationToken)
        => await _emailVerificationService.SendVerificationEmailAsync(
            notification.Email,
            notification.UserId,
            notification.ConfirmationToken,
            cancellationToken);
}
