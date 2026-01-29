using Microsoft.EntityFrameworkCore;
using SpotifyClone.Accounts.Domain.Aggregates.Users;
using SpotifyClone.Accounts.Infrastructure.Persistence.Accounts.Database;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Infrastructure.Persistence.Accounts.Repositories;

internal sealed class UserProfileEfCoreRepository(
    AccountsAppDbContext context)
    : IUserProfileRepository
{
    private readonly DbSet<UserProfile> _users = context.UserProfiles;

    public async Task<UserProfile?> GetByUserIdAsync(
        UserId userId,
        CancellationToken cancellationToken = default)
        => await _users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

    public async Task AddAsync(
        UserProfile user,
        CancellationToken cancellationToken = default)
        => await _users.AddAsync(user, cancellationToken);
}
