using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.VerifyPhoneNumber;

internal sealed class VerifyPhoneNumberCommandHandler(
    IAccountVerificationService accountVerificationService)
    : ICommandHandler<VerifyPhoneNumberCommand>
{
    private readonly IAccountVerificationService _accountVerificationService = accountVerificationService;

    public async Task<Result> Handle(
        VerifyPhoneNumberCommand request,
        CancellationToken cancellationToken)
        => await _accountVerificationService.VerifyPhoneNumberAsync(
            request.UserId,
            request.PhoneNumber,
            request.Code,
            cancellationToken);
}
