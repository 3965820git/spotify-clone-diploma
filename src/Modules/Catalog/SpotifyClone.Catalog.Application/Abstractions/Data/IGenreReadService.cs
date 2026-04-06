using SpotifyClone.Catalog.Application.Features.Genres.Queries;
using SpotifyClone.Catalog.Domain.Aggregates.Genres.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Pagination;

namespace SpotifyClone.Catalog.Application.Abstractions.Data;

public interface IGenreReadService
{
    Task<bool> ExistsAsync(
        GenreId id,
        CancellationToken cancellationToken = default);

    Task<GenreDetails?> GetDetailsAsync(
        GenreId id,
        CancellationToken cancellationToken = default);

    Task<PagedList<GenreSummary>> GetList(
        PaginationParams pagination,
        CancellationToken cancellationToken = default);
}
