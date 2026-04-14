using SpotifyClone.Accounts.Application.Abstractions;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.Login.Google;

public sealed record LoginUserWithGoogleCommand
    : IAccountsPersistentCommand<LoginUserCommandResult>;
