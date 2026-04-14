using SpotifyClone.Accounts.Application.Abstractions;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.Login.Otp.Verify;

public sealed record VerifyOtpLoginCommand(
    string PhoneNumber,
    string Code)
    : IAccountsPersistentCommand<LoginUserCommandResult>;
