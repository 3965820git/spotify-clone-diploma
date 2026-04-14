using Microsoft.EntityFrameworkCore;
using SpotifyClone.Billing.Domain.Aggregates.Subscriptions;
using SpotifyClone.Shared.BuildingBlocks.Infrastructure.Persistence.Database;

namespace SpotifyClone.Billing.Infrastructure.Persistence.Database;

public sealed class BillingAppDbContext(DbContextOptions<BillingAppDbContext> options)
    : ApplicationDbContext<BillingAppDbContext>("billing", options)
{
    public DbSet<Subscription> Subscriptions => Set<Subscription>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BillingAppDbContext).Assembly);
    }
}
