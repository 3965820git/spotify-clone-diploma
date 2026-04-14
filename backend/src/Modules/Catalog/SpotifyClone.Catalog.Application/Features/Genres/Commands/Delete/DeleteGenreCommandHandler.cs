using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Genres;
using SpotifyClone.Catalog.Domain.Aggregates.Genres.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Catalog.Application.Features.Genres.Commands.Delete;

internal sealed class DeleteGenreCommandHandler(
    ICatalogUnitOfWork unit)
    : ICommandHandler<DeleteGenreCommand, DeleteGenreCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;

    public async Task<Result<DeleteGenreCommandResult>> Handle(
        DeleteGenreCommand request,
        CancellationToken cancellationToken)
    {
        Genre? genre = await _unit.Genres.GetByIdAsync(
            GenreId.From(request.GenreId),
            cancellationToken);
        if (genre is null)
        {
            return Result.Failure<DeleteGenreCommandResult>(GenreErrors.NotFound);
        }

        genre.PrepareForDeletion();
        await _unit.Genres.DeleteAsync(genre, cancellationToken);

        return new DeleteGenreCommandResult();
    }
}
