using Microsoft.EntityFrameworkCore;
using SpotifyClone.Accounts.Application.Abstractions.Repositories;
using SpotifyClone.Accounts.Infrastructure.Persistence.Accounts.Database;
using SpotifyClone.Shared.BuildingBlocks.Application.Outbox;

namespace SpotifyClone.Accounts.Infrastructure.Persistence.Accounts.Repositories;

internal sealed class OutboxEfCoreRepository(
    AccountsAppDbContext context)
    : IOutboxRepository
{
    private readonly AccountsAppDbContext _context = context;

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
