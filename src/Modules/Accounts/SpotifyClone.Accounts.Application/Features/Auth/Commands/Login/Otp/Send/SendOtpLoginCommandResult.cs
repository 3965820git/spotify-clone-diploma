namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.Login.Otp.Send;

public sealed record SendOtpLoginCommandResult(
    double ExpiresInSeconds);
