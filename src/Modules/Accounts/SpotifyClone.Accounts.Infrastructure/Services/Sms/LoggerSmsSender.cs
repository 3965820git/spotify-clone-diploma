using Microsoft.Extensions.Logging;
using SpotifyClone.Accounts.Application.Abstractions.Services;

namespace SpotifyClone.Accounts.Infrastructure.Services.Sms;

internal sealed class LoggerSmsSender(
    ILogger<LoggerSmsSender> logger) : ISmsSender
{
    private readonly ILogger<LoggerSmsSender> _logger = logger;

    public async Task SendAsync(
        string to,
        string message,
        CancellationToken cancellationToken = default)
        => _logger.LogInformation("SMS to {PhoneNumber}: {Message}", to, message);
}
