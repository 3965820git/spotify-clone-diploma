namespace SpotifyClone.Api.Contracts.v1.Accounts.Auth.PasswordReset;

public sealed record RequestUserPasswordResetResponse(
    double ExpiresInSeconds,
    double ResendAvailableInSeconds);
