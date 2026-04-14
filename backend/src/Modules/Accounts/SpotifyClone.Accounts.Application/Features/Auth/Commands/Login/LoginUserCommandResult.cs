namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.Login;

public sealed record LoginUserCommandResult(
    string AccessToken,
    DateTimeOffset ExpiresAt,
    string RefreshToken);
