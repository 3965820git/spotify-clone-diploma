namespace SpotifyClone.Api.Contracts.v1.Accounts.Auth.Login;

public sealed record LoginUserResponse(
    string AccessToken,
    DateTimeOffset ExpiresAt);
