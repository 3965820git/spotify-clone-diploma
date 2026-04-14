namespace SpotifyClone.Api.Contracts.v1.Accounts.Auth.PasswordReset;

public sealed record VerifyUserPasswordResetRequest
{
    public required string Email { get; init; }
    public required string Code { get; init; }
}
