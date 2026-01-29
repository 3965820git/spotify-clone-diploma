namespace SpotifyClone.Api.Contracts.v1.Accounts.Auth.VerifyEmail;

public sealed record VerifyEmailRequest
{
    public required Guid UserId { get; init; }
    public required string Code { get; init; }
}
