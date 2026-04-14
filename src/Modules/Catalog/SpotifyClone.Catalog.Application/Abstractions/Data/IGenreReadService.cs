using SpotifyClone.Catalog.Application.Features.Genres.Queries;
using SpotifyClone.Catalog.Domain.Aggregates.Genres.ValueObjects;

namespace SpotifyClone.Catalog.Application.Abstractions.Data;

public interface IGenreReadService
{
    Task<bool> ExistsAsync(
        GenreId id,
        CancellationToken cancellationToken = default);

    Task<GenreDetails?> GetDetailsAsync(
        GenreId id,
        CancellationToken cancellationToken = default);
}
