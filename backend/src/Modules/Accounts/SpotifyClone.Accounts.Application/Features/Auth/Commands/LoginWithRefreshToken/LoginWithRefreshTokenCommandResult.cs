namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.LoginWithRefreshToken;

public sealed record LoginWithRefreshTokenCommandResult(
    string AccessToken,
    string RefreshToken);
