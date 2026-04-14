using SpotifyClone.Accounts.Application.Abstractions;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.Login.RefreshToken;

public sealed record LoginUserWithRefreshTokenCommand(
    string RawToken)
    : IAccountsPersistentCommand<LoginUserCommandResult>;
