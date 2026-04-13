using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.ResetPassword.Verify;

public sealed record VerifyUserPasswordResetCommand(
    string Email,
    string Code)
    : ICommand<VerifyUserPasswordResetCommandResult>;
