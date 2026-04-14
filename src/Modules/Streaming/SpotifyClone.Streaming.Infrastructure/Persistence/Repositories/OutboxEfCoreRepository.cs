using Microsoft.EntityFrameworkCore;
using SpotifyClone.Shared.BuildingBlocks.Application.Outbox;
using SpotifyClone.Streaming.Application.Abstractions.Repositories;
using SpotifyClone.Streaming.Infrastructure.Persistence.Database;

namespace SpotifyClone.Streaming.Infrastructure.Persistence.Repositories;

internal sealed class OutboxEfCoreRepository(
    StreamingAppDbContext context)
    : IOutboxRepository
{
    private readonly StreamingAppDbContext _context = context;

    public async Task AddAsync(
        OutboxMessage outboxMessage,
        CancellationToken cancellationToken = default)
        => await _context.AddAsync(outboxMessage, cancellationToken);

    public async Task<IEnumerable<OutboxMessage>> GetPendings(
        CancellationToken cancellationToken = default)
        => await _context.OutboxMessages
            .Where(m => m.ProcessedOn == null)
            .OrderBy(m => m.OccurredOn)
            .Take(20)
            .ToListAsync(cancellationToken);
}
