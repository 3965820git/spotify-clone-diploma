using Microsoft.EntityFrameworkCore;
using SpotifyClone.Billing.Application.Abstractions.Repositories;
using SpotifyClone.Billing.Infrastructure.Persistence.Database;
using SpotifyClone.Shared.BuildingBlocks.Application.Outbox;

namespace SpotifyClone.Billing.Infrastructure.Persistence.Repositories;

internal sealed class OutboxEfCoreRepository(
    BillingAppDbContext context)
    : IOutboxRepository
{
    private readonly BillingAppDbContext _context = context;

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
