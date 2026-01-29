using Microsoft.EntityFrameworkCore;
using SpotifyClone.Accounts.Domain.Aggregates.Users;
using SpotifyClone.Accounts.Infrastructure.Persistence.Auth;
using SpotifyClone.Shared.BuildingBlocks.Infrastructure.Persistence.Database;

namespace SpotifyClone.Accounts.Infrastructure.Persistence.Accounts.Database;

public sealed class AccountsAppDbContext(DbContextOptions<AccountsAppDbContext> options)
    : ApplicationDbContext<AccountsAppDbContext>("accounts", options)
{
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountsAppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
