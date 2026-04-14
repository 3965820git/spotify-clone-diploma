using MediatR;
using Microsoft.Extensions.Logging;
using SpotifyClone.Shared.IntegrationEvents.Catalog.Albums;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Application.Abstractions;
using SpotifyClone.Streaming.Domain.Aggregates.ImageAssets;

namespace SpotifyClone.Streaming.Application.EventHandlers.ImageAssets;

internal sealed class ImageLinkAddedIntegrationEventHandler(
    IStreamingUnitOfWork unit,
    ILogger<ImageLinkAddedIntegrationEventHandler> logger)
    : INotificationHandler<ImageLinkAddedIntegrationEvent>
{
    private readonly IStreamingUnitOfWork _unit = unit;
    private readonly ILogger<ImageLinkAddedIntegrationEventHandler> _logger = logger;

    public async Task Handle(
        ImageLinkAddedIntegrationEvent notification,
        CancellationToken cancellationToken)
    {
        ImageAsset? imageAsset = await _unit.ImageAssets.GetByIdAsync(
            ImageId.From(notification.ImageId), cancellationToken);
        if (imageAsset is null)
        {
            _logger.LogError(
                "Image asset with ID {ImageId} not found while adding new link to it",
                notification.ImageId);

            throw new InvalidOperationException(
                $"Image asset with ID {notification.ImageId} not found");
        }

        imageAsset.AddLink();

        await _unit.CommitAsync(cancellationToken);
    }
}
