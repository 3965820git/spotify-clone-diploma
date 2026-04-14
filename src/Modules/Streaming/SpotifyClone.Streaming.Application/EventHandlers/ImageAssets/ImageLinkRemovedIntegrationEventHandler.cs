using MediatR;
using Microsoft.Extensions.Logging;
using SpotifyClone.Shared.IntegrationEvents.Catalog.Albums;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Application.Abstractions;
using SpotifyClone.Streaming.Domain.Aggregates.ImageAssets;

namespace SpotifyClone.Streaming.Application.EventHandlers.ImageAssets;

internal sealed class ImageLinkRemovedIntegrationEventHandler(
    IStreamingUnitOfWork unit,
    ILogger<ImageLinkRemovedIntegrationEventHandler> logger)
    : INotificationHandler<ImageLinkRemovedIntegrationEvent>
{
    private readonly IStreamingUnitOfWork _unit = unit;
    private readonly ILogger<ImageLinkRemovedIntegrationEventHandler> _logger = logger;

    public async Task Handle(
        ImageLinkRemovedIntegrationEvent notification,
        CancellationToken cancellationToken)
    {
        ImageAsset? imageAsset = await _unit.ImageAssets.GetByIdAsync(
            ImageId.From(notification.ImageId), cancellationToken);

        if (imageAsset is null)
        {
            _logger.LogWarning(
                "Image asset with ID {ImageId} not found while removing a link from it",
                notification.ImageId);

            throw new InvalidOperationException(
                $"Image asset with ID {notification.ImageId} not found");
        }

        imageAsset.RemoveLink();

        await _unit.CommitAsync(cancellationToken);
    }
}
