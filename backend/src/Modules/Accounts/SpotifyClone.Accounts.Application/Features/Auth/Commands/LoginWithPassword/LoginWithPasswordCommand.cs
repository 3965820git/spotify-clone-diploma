using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.LoginWithPassword;

public sealed record LoginWithPasswordCommand(
    string Email,
    string Password)
    : IPersistentCommand<LoginWithPasswordCommandResult>;
