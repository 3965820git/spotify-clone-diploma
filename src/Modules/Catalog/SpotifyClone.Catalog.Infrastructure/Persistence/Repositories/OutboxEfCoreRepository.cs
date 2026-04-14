using Microsoft.EntityFrameworkCore;
using SpotifyClone.Catalog.Application.Abstractions.Repositories;
using SpotifyClone.Catalog.Infrastructure.Persistence.Database;
using SpotifyClone.Shared.BuildingBlocks.Application.Outbox;

namespace SpotifyClone.Catalog.Infrastructure.Persistence.Repositories;

internal sealed class OutboxEfCoreRepository(
    CatalogAppDbContext context)
    : IOutboxRepository
{
    private readonly CatalogAppDbContext _context = context;

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
