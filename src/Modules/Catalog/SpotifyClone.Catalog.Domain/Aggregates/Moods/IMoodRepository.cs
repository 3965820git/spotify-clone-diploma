using SpotifyClone.Catalog.Domain.Aggregates.Moods.ValueObjects;

namespace SpotifyClone.Catalog.Domain.Aggregates.Moods;

public interface IMoodRepository
{
    Task<bool> IsNameUniqueAsync(
        string name,
        CancellationToken cancellationToken = default);

    Task<Mood?> GetByIdAsync(
        MoodId id,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Mood>> GetByIdsAsync(
        IEnumerable<MoodId> ids,
        CancellationToken cancellationToken = default);

    Task<bool> Exists(
        MoodId id,
        CancellationToken cancellationToken = default);

    Task<bool> Exists(
        IEnumerable<MoodId> ids,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Mood mood,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Mood mood,
        CancellationToken cancellationToken = default);
}
