namespace SpotifyClone.Api.Contracts.v1.Accounts.Auth.Login;

public sealed record LoginUserWithPasswordRequest
{
    public required string Identifier { get; init; }
    public required string Password { get; init; }
}
