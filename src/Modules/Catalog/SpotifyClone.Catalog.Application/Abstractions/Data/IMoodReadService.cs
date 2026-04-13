using SpotifyClone.Catalog.Application.Features.Moods.Queries;
using SpotifyClone.Catalog.Domain.Aggregates.Moods.ValueObjects;

namespace SpotifyClone.Catalog.Application.Abstractions.Data;

public interface IMoodReadService
{
    Task<bool> ExistsAsync(
        MoodId id,
        CancellationToken cancellationToken = default);

    Task<MoodDetails?> GetDetailsAsync(
        MoodId id,
        CancellationToken cancellationToken = default);
}
