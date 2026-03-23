using SpotifyClone.Catalog.Application.Features.Artists.Queries;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.ValueObjects;

namespace SpotifyClone.Catalog.Application.Abstractions.Data;

public interface IArtistReadService
{
    Task<bool> ExistsAsync(
        ArtistId id,
        CancellationToken cancellationToken = default);

    Task<ArtistDetails?> GetDetailsAsync(
        ArtistId id,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<ArtistSummary>> GetAllAsync(
        IEnumerable<ArtistId> artistIds,
        CancellationToken cancellationToken = default);
}
