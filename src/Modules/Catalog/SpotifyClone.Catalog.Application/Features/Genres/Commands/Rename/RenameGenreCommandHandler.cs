using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Genres;
using SpotifyClone.Catalog.Domain.Aggregates.Genres.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Catalog.Application.Features.Genres.Commands.Rename;

internal sealed class RenameGenreCommandHandler(
    ICatalogUnitOfWork unit)
    : ICommandHandler<RenameGenreCommand, RenameGenreCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;

    public async Task<Result<RenameGenreCommandResult>> Handle(
        RenameGenreCommand request,
        CancellationToken cancellationToken)
    {
        Genre? genre = await _unit.Genres.GetByIdAsync(
            GenreId.From(request.GenreId), cancellationToken);
        if (genre is null)
        {
            return Result.Failure<RenameGenreCommandResult>(GenreErrors.NotFound);
        }

        if (!await _unit.Genres.IsNameUniqueAsync(request.Name, cancellationToken))
        {
            return Result.Failure<RenameGenreCommandResult>(GenreErrors.InvalidName);
        }

        genre.Rename(request.Name);

        return new RenameGenreCommandResult();
    }
}
