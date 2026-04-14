namespace SpotifyClone.Accounts.Application.Abstractions.Services;

public interface ISmsSender
{
    Task SendAsync(
        string to,
        string message,
        CancellationToken cancellationToken = default);
}
