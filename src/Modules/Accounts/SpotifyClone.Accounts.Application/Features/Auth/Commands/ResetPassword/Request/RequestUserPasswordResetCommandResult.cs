namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.ResetPassword.Request;

public sealed record RequestUserPasswordResetCommandResult(
    double ExpiresInSeconds,
    double ResendAvailableInSeconds);
