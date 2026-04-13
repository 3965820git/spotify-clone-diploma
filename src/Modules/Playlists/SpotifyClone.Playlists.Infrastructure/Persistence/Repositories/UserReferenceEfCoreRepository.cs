using Microsoft.EntityFrameworkCore;
using SpotifyClone.Playlists.Application.Abstractions.Repositories;
using SpotifyClone.Playlists.Infrastructure.Persistence.Database;
using SpotifyClone.Playlists.Infrastructure.Persistence.Entities;

namespace SpotifyClone.Playlists.Infrastructure.Persistence.Repositories;

internal sealed class UserReferenceEfCoreRepository(
    PlaylistsAppDbContext context)
    : IUserReferenceRepository
{
    private readonly DbSet<UserReference> _users = context.UserReferences;

    public async Task<bool> ExistsAsync(
        Guid userId,
        CancellationToken cancellationToken)
        => await _users.AnyAsync(u => u.Id == userId, cancellationToken);

    public async Task AddAsync(
        Guid userId,
        string name,
        Guid? avatarImageId,
        CancellationToken cancellationToken = default)
        => await _users.AddAsync(
            new UserReference(userId, name, avatarImageId),
            cancellationToken);

    public async Task DeleteAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
        => await _users
        .Where(u => u.Id == userId)
        .ExecuteDeleteAsync(cancellationToken);
}
