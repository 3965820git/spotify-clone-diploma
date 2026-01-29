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

    public virtual async Task<int> Commit(CancellationToken cancellationToken = default)
    {
        var domainEntities = DbContext.ChangeTracker
            .Entries<IHasDomainEvents>()
            .Where(x => x.Entity.DomainEvents.Count != 0)
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ForEach(x => x.Entity.ClearDomainEvents());

        int result = await DbContext.SaveChangesAsync(cancellationToken);

        foreach (DomainEvent? domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent, cancellationToken);
        }

        return result;
    }
}
