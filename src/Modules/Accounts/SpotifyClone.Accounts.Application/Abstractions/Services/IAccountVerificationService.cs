using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Accounts.Application.Abstractions.Services;

public interface IAccountVerificationService
{
    Task<Result> SendEmailChangedEmailAsync(
        string oldEmail,
        string newEmail,
        string displayName,
        CancellationToken cancellationToken = default);

    Task<Result> SendVerificationEmailAsync(
        string email,
        Guid userId,
        string token,
        CancellationToken cancellationToken = default);

    Task<Result> VerifyEmailAsync(
        Guid userId,
        string token,
        CancellationToken cancellationToken = default);

    Task<Result> SendOtpAsync(
        Guid userId,
        string phoneNumber,
        CancellationToken cancellationToken = default);

    Task<Result> VerifyOtpAsync(
        Guid userId,
        string phoneNumber,
        string token,
        CancellationToken cancellationToken = default);

    Task<Result<TimeSpan>> SendPasswordResetEmailAsync(
        string email,
        CancellationToken cancellationToken = default);

    Task<Result> VerifyPasswordResetTokenAsync(
        string email,
        string token,
        CancellationToken cancellationToken = default);

    Task<Result> ConfirmPasswordResetAsync(
        string email,
        string token,
        string newPassword,
        CancellationToken cancellationToken = default);
}
