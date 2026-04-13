using Microsoft.Extensions.Logging;
using SpotifyClone.Catalog.Application.Abstractions.Data;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Genres.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Catalog.Application.Features.Genres.Queries.GetDetails;

internal sealed class GetGenreDetailsQueryHandler(
    IGenreReadService genreReadService,
    ILogger<GetGenreDetailsQueryHandler> logger)
    : IQueryHandler<GetGenreDetailsQuery, GenreDetails>
{
    private readonly IGenreReadService _genreReadService = genreReadService;
    private readonly ILogger<GetGenreDetailsQueryHandler> _logger = logger;

    public async Task<Result<GenreDetails>> Handle(
        GetGenreDetailsQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting Genre info {GenreId}", request.GenreId);

        GenreDetails? genre = await _genreReadService.GetDetailsAsync(
            GenreId.From(request.GenreId),
            cancellationToken);

        if (genre is null)
        {
            _logger.LogWarning(
                "Genre {GenreId} not found", request.GenreId);

            return Result.Failure<GenreDetails>(GenreErrors.NotFound);
        }

        return genre;
    }
}
