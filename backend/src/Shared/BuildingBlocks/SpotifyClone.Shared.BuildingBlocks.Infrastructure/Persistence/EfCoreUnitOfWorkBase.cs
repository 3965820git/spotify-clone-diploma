using MediatR;
using Microsoft.EntityFrameworkCore;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Shared.BuildingBlocks.Infrastructure.Persistence;

public abstract class EfCoreUnitOfWorkBase<TDbContext>(
    TDbContext dbContext,
    IPublisher publisher)
    : IUnitOfWork
    where TDbContext : DbContext
{
    protected TDbContext DbContext { get; } = dbContext;
    protected IPublisher Publisher { get; } = publisher;

    public virtual async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = DbContext.ChangeTracker
        .Entries<IHasDomainEvents>()
        .Select(x => x.Entity)
        .SelectMany(x => {
            var events = x.DomainEvents.ToList();
            x.ClearDomainEvents();
            return events;
        }).ToList();

        // Domain events are not stored for now

        //var outboxMessages = domainEvents.Select(domainEvent => new OutboxMessage(
        //    domainEvent.GetType().Name,
        //    JsonSerializer.Serialize(domainEvent, domainEvent.GetType())))
        //.ToList();

        //DbContext.Set<OutboxMessage>().AddRange(outboxMessages);

        int result = await DbContext.SaveChangesAsync(cancellationToken);

        foreach (DomainEvent? domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent, cancellationToken);
        }

        return result;
    }
}
