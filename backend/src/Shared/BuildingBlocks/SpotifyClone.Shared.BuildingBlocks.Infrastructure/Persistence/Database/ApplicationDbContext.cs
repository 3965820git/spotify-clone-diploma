using Microsoft.EntityFrameworkCore;

namespace SpotifyClone.Shared.BuildingBlocks.Infrastructure.Persistence.Database;

public abstract class ApplicationDbContext<TDbContext>(string schema, DbContextOptions<TDbContext> options)
    : DbContext(options)
    where TDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(schema);
    }
}
