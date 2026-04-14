namespace SpotifyClone.Playlists.Application.Abstractions.Repositories;

public interface IUserReferenceRepository
{
    Task<bool> ExistsAsync(
        Guid userId,
        CancellationToken cancellationToken);

    Task AddAsync(
        Guid userId,
        string name,
        Guid? avatarImageId,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}
