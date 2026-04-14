using SpotifyClone.Shared.BuildingBlocks.Application.Outbox;

namespace SpotifyClone.Streaming.Application.Abstractions.Repositories;

public interface IOutboxRepository
{
    Task AddAsync(
        OutboxMessage outboxMessage,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<OutboxMessage>> GetPendings(
        CancellationToken cancellationToken = default);
}
