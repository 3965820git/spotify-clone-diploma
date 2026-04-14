using Microsoft.AspNetCore.SignalR;

namespace SpotifyClone.Streaming.Infrastructure.Notifications;

public sealed class StreamingHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        string? userId = Context.UserIdentifier;
        string? deviceId = Context.GetHttpContext()?.Request.Query["deviceId"];

        if (!string.IsNullOrWhiteSpace(userId) && !string.IsNullOrWhiteSpace(deviceId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"{userId}_{deviceId}");
        }

        await base.OnConnectedAsync();
    }
}
