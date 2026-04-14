using System.Text.Json;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Services;

namespace SpotifyClone.Shared.BuildingBlocks.Application.Outbox;

public sealed class OutboxMessage(
    string type,
    string content)
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Type { get; init; } = type;
    public string Content { get; init; } = content;
    public DateTimeOffset OccurredOn { get; init; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ProcessedOn { get; private set; }
    public string? Error { get; private set; }

    public static OutboxMessage FromIntegrationEvent(IntegrationEvent integrationEvent)
        => new OutboxMessage(
            IntegrationEventTypeRegistry.GetKeyForType(integrationEvent.GetType()),
            JsonSerializer.Serialize(
            integrationEvent,
            integrationEvent.GetType()));

    public void MarkAsProcessed() => ProcessedOn = DateTimeOffset.UtcNow;

    public void MarkAsFailed(string error) => Error = error;
}
