using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.VerifyEmail;

internal sealed class VerifyEmailCommandHandler(
    IAccountVerificationService accountVerificationService)
    : ICommandHandler<VerifyEmailCommand>
{
    private readonly IAccountVerificationService _accountVerificationService = accountVerificationService;

    public async Task<Result> Handle(
        VerifyEmailCommand request,
        CancellationToken cancellationToken)
        => await _accountVerificationService.VerifyEmailAsync(
            request.UserId,
            request.Code,
            cancellationToken);
}
