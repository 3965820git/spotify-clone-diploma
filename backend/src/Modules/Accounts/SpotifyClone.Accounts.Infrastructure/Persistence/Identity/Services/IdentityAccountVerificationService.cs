using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Accounts.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Application.Configuration;
using SpotifyClone.Shared.BuildingBlocks.Application.Email;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Accounts.Infrastructure.Persistence.Identity.Services;

internal sealed class IdentityAccountVerificationService(
    ILogger<IdentityAccountVerificationService> logger,
    IIdentityService identity,
    IEmailSender emailSender,
    ISmsSender smsSender,
    IOptions<ApplicationSettings> appSettings)
    : IAccountVerificationService
{
    private readonly ILogger<IdentityAccountVerificationService> _logger = logger;
    private readonly IIdentityService _identity = identity;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly ISmsSender _smsSender = smsSender;
    private readonly ApplicationSettings _appSettings = appSettings.Value;

    public async Task<Result> SendEmailChangedEmailAsync(
        string oldEmail,
        string newEmail,
        string displayName,
        CancellationToken cancellationToken = default)
    {
        var emailMessage = new EmailMessage(
            [oldEmail],
            $"Вашу електронну адресу {_appSettings.DomainName} змінено",
            PlainTextBody: $"Привіт, {displayName},\n\n" +
            $"Електронну адресу вашого облікового запису Spotify нещодавно було змінено.\n\n" +
            $"Ознайомтеся з деталями цих оновлень облікового запису нижче:\n\n" +
            $"Стара електронна адреса:\n{oldEmail}\n\n" +
            $"Нова електронна адреса:\n{newEmail}\n\n" +
            $"Якщо ви внесли цю зміну, подальші дії не потрібні.\n\n" +
            $"Ви не вносили цю зміну? Негайно скасуйте ці зміни у вашому обліковому записі {_appSettings.DomainName}." +
            $"\n\nБудь ласка, не пересилайте цей лист.\n\n" +
            $"Забезпечення вашої безпеки,\n{_appSettings.DomainName} Security");

        _logger.LogInformation("Sending email changed email...");

        Result result = await _emailSender.SendAsync(emailMessage, cancellationToken: cancellationToken);
        if (result.IsFailure)
        {
            _logger.LogError("Sending email changed email failed.");
        }
        else
        {
            _logger.LogInformation("Sending email changed email succeeded.");
        }

        return result;
    }

    public async Task<Result> SendVerificationEmailAsync(
        string email,
        Guid userId,
        string token,
        CancellationToken cancellationToken = default)
    {
        var emailMessage = new EmailMessage(
            [email],
            $"{_appSettings.DomainName} - Підтвердження пошти",
            GetRegistrationHtml(token));

        _logger.LogInformation("Sending verification email...");

        Result result = await _emailSender.SendAsync(emailMessage, cancellationToken: cancellationToken);
        if (result.IsFailure)
        {
            _logger.LogError("Sending verification email failed.");
        }
        else
        {
            _logger.LogInformation("Sending verification email succeeeded.");
        }

        return result;
    }

    public async Task<Result> VerifyEmailAsync(
        Guid userId,
        string token,
        CancellationToken cancellationToken = default)
    {
        Result result = await _identity.ConfirmEmailAsync(userId, token);

        if (result.IsFailure)
        {
            _logger.LogError("Confirming email failed, {Error}", result.Errors.Select(e => e.ToString()));
        }
        else
        {
            _logger.LogInformation("Confirming email succeeeded.");
        }

        return result;
    }

    public async Task<Result> SendOtpAsync(
        Guid userId,
        string phoneNumber,
        CancellationToken cancellationToken = default)
    {
        Result<string> tokenResult = await _identity.GeneratePhoneNumberConfirmationTokenAsync(userId, phoneNumber);
        if (tokenResult.IsFailure)
        {
            return Result.Failure(tokenResult.Errors);
        }

        _logger.LogInformation("Sending One-Time-Password SMS...");

        string message = $"Твій {_appSettings.DomainName} код: {tokenResult.Value}";
        await _smsSender.SendAsync(phoneNumber, message, cancellationToken);
        return Result.Success();
    }
    
    public async Task<Result> VerifyOtpAsync(
        Guid userId,
        string phoneNumber,
        string token,
        CancellationToken cancellationToken = default)
    {
        Result result = await _identity.ConfirmPhoneNumberAsync(userId, phoneNumber, token);

        if (result.IsFailure)
        {
            _logger.LogError("Confirming phone number failed, {Error}", result.Errors.Select(e => e.ToString()));
        }
        else
        {
            _logger.LogInformation("Confirming phone number succeeeded.");
        }

        return result;
    }

    public async Task<Result<TimeSpan>> SendPasswordResetEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        Result<string> tokenResult = await _identity.GeneratePasswordResetTokenAsync(email, cancellationToken);
        if (tokenResult.IsFailure)
        {
            return Result.Failure<TimeSpan>(tokenResult.Errors);
        }

        var message = new EmailMessage(
            [email],
            $"{_appSettings.DomainName} - обновлення паролю",
            null,
            $"Твій {_appSettings.DomainName} код для обновлення паролю: {tokenResult.Value}");
        Result sendResult = await _emailSender.SendAsync(message, cancellationToken: cancellationToken);
        if (sendResult.IsFailure)
        {
            _logger.LogError(
                "Sending password reset email failed, {Error}",
                sendResult.Errors.Select(e => e.ToString()));
        }
        else
        {
            _logger.LogInformation("Sending password reset email succeeeded.");
        }

        return TimeSpan.FromSeconds(180);
    }

    public async Task<Result> VerifyPasswordResetTokenAsync(
        string email,
        string token,
        CancellationToken cancellationToken = default)
    {
        Result<bool> verificationResult = await _identity.VerifyPasswordResetTokenAsync(
            email, token, cancellationToken);
        if (verificationResult.IsFailure)
        {
            return verificationResult;
        }

        if (!verificationResult.Value)
        {
            return Result.Failure(AuthErrors.InvalidPasswordResetToken);
        }

        return verificationResult;
    }

    public async Task<Result> ConfirmPasswordResetAsync(
        string email,
        string token,
        string newPassword,
        CancellationToken cancellationToken = default)
        => await _identity.ConfirmPasswordResetTokenAsync(
            email, token, newPassword, cancellationToken);

    private static string GetRegistrationHtml(string token)
        => $$"""
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>Підтвердження реєстрації</title>
        <style>
            body { font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; background-color: #121212; color: #ffffff; margin: 0; padding: 0; }
            .container { max-width: 600px; margin: 0 auto; padding: 40px 20px; }
            .logo { text-align: center; margin-bottom: 30px; font-size: 28px; font-weight: bold; color: #1DB954; }
            .card { background-color: #181818; padding: 40px; border-radius: 8px; text-align: center; border: 1px solid #282828; }
            h1 { font-size: 24px; margin-bottom: 20px; color: #ffffff; }
            p { color: #b3b3b3; line-height: 1.6; margin-bottom: 30px; }
            .token { background-color: #1DB954; color: #ffffff !important; padding: 14px 48px; border-radius: 50px; text-decoration: none; font-weight: bold; display: inline-block; }
            .footer { margin-top: 30px; text-align: center; color: #535353; font-size: 12px; }
            .link-alt { color: #1DB954; text-decoration: none; word-break: break-all; }
        </style>
    </head>
    <body>
        <div class="container">
            <div class="logo">SpotifyClone</div>
            <div class="card">
                <h1>Підтверди свій e-mail</h1>
                <p>Вітаємо у SpotifyClone! Ми раді, що ти з нами. Щоб отримати повний доступ до всіх функцій, будь ласка, скопіюй і введи цей код.</p>
                <span class="token">{{token}}</span>
            </div>
            <div class="footer">
                &copy; 2026 SpotifyClone Inc. <br>
                Ви отримали цей лист, тому що зареєструвалися у нашому сервісі.
            </div>
        </div>
    </body>
    </html>
    """;
}
