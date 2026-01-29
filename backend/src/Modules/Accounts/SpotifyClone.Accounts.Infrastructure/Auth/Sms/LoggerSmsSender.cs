using Microsoft.Extensions.Logging;
using SpotifyClone.Accounts.Application.Abstractions.Services;

namespace SpotifyClone.Accounts.Infrastructure.Auth.Sms;

internal sealed class LoggerSmsSender(
    ILogger<LoggerSmsSender> logger) : ISmsSender
{
    private readonly ILogger<LoggerSmsSender> _logger = logger;

    public async Task SendAsync(
        string to,
        string message)
        => logger.LogInformation("SMS to {PhoneNumber}: {Message}", to, message);
}
