namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.LoginWithPassword;

public sealed record LoginWithPasswordCommandResult(
    string AccessToken,
    string RefreshToken);
