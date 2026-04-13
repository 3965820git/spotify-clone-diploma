using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.ResetPassword.Confirm;

internal sealed class ConfirmUserPasswordResetCommandHandler(
    IAccountVerificationService verificationService)
    : ICommandHandler<ConfirmUserPasswordResetCommand, ConfirmUserPasswordResetCommandResult>
{
    private readonly IAccountVerificationService _verificationService = verificationService;

    public async Task<Result<ConfirmUserPasswordResetCommandResult>> Handle(
        ConfirmUserPasswordResetCommand request,
        CancellationToken cancellationToken)
    {
        Result verificationResult = await _verificationService.ConfirmPasswordResetAsync(
            request.Email, request.Code, request.NewPassword, cancellationToken);
        if (verificationResult.IsFailure)
        {
            return Result.Failure<ConfirmUserPasswordResetCommandResult>(verificationResult.Errors);
        }

        return new ConfirmUserPasswordResetCommandResult();
    }
}
