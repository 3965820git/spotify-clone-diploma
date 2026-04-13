using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Genres;
using SpotifyClone.Catalog.Domain.Aggregates.Genres.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Catalog.Application.Features.Genres.Commands.UnlinkCover;

internal sealed class UnlinkCoverFromGenreCommandHandler(
    ICatalogUnitOfWork unit)
    : ICommandHandler<UnlinkCoverFromGenreCommand, UnlinkCoverFromGenreCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;

    public async Task<Result<UnlinkCoverFromGenreCommandResult>> Handle(
        UnlinkCoverFromGenreCommand request,
        CancellationToken cancellationToken)
    {
        Genre? genre = await _unit.Genres.GetByIdAsync(
            GenreId.From(request.GenreId),
            cancellationToken);
        if (genre is null)
        {
            return Result.Failure<UnlinkCoverFromGenreCommandResult>(GenreErrors.NotFound);
        }

        genre.TryUnlinkCover();

        return new UnlinkCoverFromGenreCommandResult();
    }
}
