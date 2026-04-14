using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;
using SpotifyClone.Playlists.Application.Abstractions;
using SpotifyClone.Playlists.Application.Abstractions.Repositories;
using SpotifyClone.Shared.BuildingBlocks.Application.Outbox;
using SpotifyClone.Shared.BuildingBlocks.Application.Services;

namespace SpotifyClone.Playlists.Application.Jobs;

public sealed class ProcessOutboxMessagesJob(
    IOutboxRepository outbox,
    IPublisher publisher,
    IPlaylistsUnitOfWork unit,
    ILogger<ProcessOutboxMessagesJob> logger)
{
    private readonly IOutboxRepository _outbox = outbox;
    private readonly IPublisher _publisher = publisher;
    private readonly IPlaylistsUnitOfWork _unit = unit;
    private readonly ILogger<ProcessOutboxMessagesJob> _logger = logger;

    public async Task ProcessAsync(
        CancellationToken cancellationToken = default)
    {
        IEnumerable<OutboxMessage> messages = await _outbox.GetPendings(cancellationToken);

        foreach (OutboxMessage message in messages)
        {
            if (message.Error is not null)
            {
                continue;
            }

            try
            {
                Type? type = IntegrationEventTypeRegistry.GetTypeFromName(message.Type);

                if (type is null)
                {
                    message.MarkAsFailed("Could not load.");
                    _logger.LogError("Could not find type {TypeName} for outbox message {Id}", message.Type, message.Id);
                    continue;
                }

                object? integrationEvent = JsonSerializer.Deserialize(message.Content, type);

                if (integrationEvent != null)
                {
                    await _publisher.Publish(integrationEvent, cancellationToken);
                    message.MarkAsProcessed();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process outbox message {Id}", message.Id);
                message.MarkAsFailed(ex.Message);
            }
        }

        await _unit.CommitAsync(cancellationToken);

        await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
    }
}
