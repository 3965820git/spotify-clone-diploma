using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.ResetPassword.Verify;

internal sealed class VerifyUserPasswordResetCommandHandler(
    IAccountVerificationService verificationService)
    : ICommandHandler<VerifyUserPasswordResetCommand, VerifyUserPasswordResetCommandResult>
{
    private readonly IAccountVerificationService _verificationService = verificationService;

    public async Task<Result<VerifyUserPasswordResetCommandResult>> Handle(
        VerifyUserPasswordResetCommand request,
        CancellationToken cancellationToken)
    {
        Result verificationResult = await _verificationService.VerifyPasswordResetTokenAsync(
            request.Email, request.Code, cancellationToken);
        if (verificationResult.IsFailure)
        {
            return Result.Failure<VerifyUserPasswordResetCommandResult>(verificationResult.Errors);
        }

        return new VerifyUserPasswordResetCommandResult();
    }
}
