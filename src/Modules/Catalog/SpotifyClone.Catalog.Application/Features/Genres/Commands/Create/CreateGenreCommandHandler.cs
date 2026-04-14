using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Genres;
using SpotifyClone.Catalog.Domain.Aggregates.Genres.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Catalog.Application.Features.Genres.Commands.Create;

internal sealed class CreateGenreCommandHandler(
    ICatalogUnitOfWork unit)
    : ICommandHandler<CreateGenreCommand, CreateGenreCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;

    public async Task<Result<CreateGenreCommandResult>> Handle(
        CreateGenreCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _unit.Genres.IsNameUniqueAsync(request.Name, cancellationToken))
        {
            return Result.Failure<CreateGenreCommandResult>(GenreErrors.InvalidName);
        }

        var genre = Genre.Create(
            GenreId.From(Guid.NewGuid()),
            request.Name);

        await _unit.Genres.AddAsync(genre, cancellationToken);

        return new CreateGenreCommandResult(genre.Id.Value);
    }
}
