namespace SpotifyClone.Api.Contracts.v1.Accounts.Auth.PasswordReset;

public sealed record RequestUserPasswordResetRequest
{
    public required string Email { get; init; }
}
