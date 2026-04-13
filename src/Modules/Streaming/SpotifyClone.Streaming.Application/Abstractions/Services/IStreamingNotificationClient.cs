using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Domain.ValueObjects;

namespace SpotifyClone.Streaming.Application.Abstractions.Services;

public interface IStreamingNotificationClient
{
    Task SendStopPlaybackSignalAsync(
        UserId userId,
        DeviceId deviceId,
        CancellationToken cancellationToken = default);
}
