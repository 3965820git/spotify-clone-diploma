using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.ResetPassword.Request;

public sealed record RequestUserPasswordResetCommand(
    string Email)
    : ICommand<RequestUserPasswordResetCommandResult>;
