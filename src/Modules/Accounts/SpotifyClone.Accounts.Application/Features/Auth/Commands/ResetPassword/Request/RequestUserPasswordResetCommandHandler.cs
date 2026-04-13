using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.ResetPassword.Request;

internal sealed class RequestUserPasswordResetCommandHandler(
    IAccountVerificationService verificationService)
    : ICommandHandler<RequestUserPasswordResetCommand, RequestUserPasswordResetCommandResult>
{
    private readonly IAccountVerificationService _verificationService = verificationService;

    public async Task<Result<RequestUserPasswordResetCommandResult>> Handle(
        RequestUserPasswordResetCommand request,
        CancellationToken cancellationToken)
    {
        Result<TimeSpan> sendResult = await _verificationService.SendPasswordResetEmailAsync(
            request.Email, cancellationToken);
        if (sendResult.IsFailure)
        {
            return Result.Failure<RequestUserPasswordResetCommandResult>(sendResult.Errors);
        }

        return new RequestUserPasswordResetCommandResult(
            sendResult.Value.TotalSeconds,
            60);
    }
}
