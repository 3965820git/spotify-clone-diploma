using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Domain.Aggregates.Users;

public interface IUserProfileRepository
{
    Task<UserProfile?> GetByUserIdAsync(
        UserId userId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        UserProfile user,
        CancellationToken cancellationToken = default);
}
