using Microsoft.AspNetCore.SignalR;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Application.Abstractions.Services;
using SpotifyClone.Streaming.Domain.ValueObjects;

namespace SpotifyClone.Streaming.Infrastructure.Notifications;

internal sealed class SignalRStreamingNotificationClient(
    IHubContext<StreamingHub> hubContext)
    : IStreamingNotificationClient
{
    private readonly IHubContext<StreamingHub> _hubContext = hubContext;

    public Task SendStopPlaybackSignalAsync(
        UserId userId,
        DeviceId deviceId,
        CancellationToken cancellationToken = default)
        => _hubContext.Clients
            .Group($"{userId.Value}_{deviceId.Value}")
            .SendAsync("ReceiveStopPlayback", cancellationToken);
}
