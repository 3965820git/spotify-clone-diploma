using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.ResetPassword.Confirm;

public sealed record ConfirmUserPasswordResetCommand(
    string Email,
    string Code,
    string NewPassword)
    : ICommand<ConfirmUserPasswordResetCommandResult>;
