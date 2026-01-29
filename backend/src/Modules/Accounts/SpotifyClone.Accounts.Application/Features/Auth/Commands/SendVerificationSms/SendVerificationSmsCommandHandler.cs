using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.SendVerificationSms;

internal sealed class SendVerificationSmsCommandHandler(
    IAccountVerificationService accountVerificationService)
    : ICommandHandler<SendVerificationSmsCommand>
{
    private readonly IAccountVerificationService _accountVerificationService = accountVerificationService;

    public async Task<Result> Handle(
        SendVerificationSmsCommand request,
        CancellationToken cancellationToken)
        => await _accountVerificationService.SendVerificationSmsAsync(
            request.UserId,
            request.PhoneNumber);
}
