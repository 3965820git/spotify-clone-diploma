using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Domain.Aggregates.Users;

public interface IUserProfileRepository
{
    Task<UserProfile?> GetByIdAsync(
        UserId id,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        UserProfile user,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        UserProfile userProfile,
        CancellationToken cancellationToken = default);
}
