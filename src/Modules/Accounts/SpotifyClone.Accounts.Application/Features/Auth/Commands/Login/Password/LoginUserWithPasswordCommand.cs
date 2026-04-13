using SpotifyClone.Accounts.Application.Abstractions;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.Login.Password;

public sealed record LoginUserWithPasswordCommand(
    string Identifier,
    string Password)
    : IAccountsPersistentCommand<LoginUserCommandResult>;
