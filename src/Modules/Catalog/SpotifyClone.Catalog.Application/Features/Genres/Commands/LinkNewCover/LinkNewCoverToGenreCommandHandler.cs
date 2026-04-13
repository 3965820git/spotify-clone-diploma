using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Genres;
using SpotifyClone.Catalog.Domain.Aggregates.Genres.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.Enums;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Application.Features.Genres.Commands.LinkNewCover;

internal sealed class LinkNewCoverToGenreCommandHandler(
    ICatalogUnitOfWork unit)
    : ICommandHandler<LinkNewCoverToGenreCommand, LinkNewCoverToGenreCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;

    public async Task<Result<LinkNewCoverToGenreCommandResult>> Handle(
        LinkNewCoverToGenreCommand request,
        CancellationToken cancellationToken)
    {
        Genre? genre = await _unit.Genres.GetByIdAsync(
            GenreId.From(request.GenreId),
            cancellationToken);
        if (genre is null)
        {
            return Result.Failure<LinkNewCoverToGenreCommandResult>(GenreErrors.NotFound);
        }

        genre.LinkNewCover(new GenreCoverImage(
            ImageId.From(request.ImageId),
            request.ImageWidth,
            request.ImageHeight,
            ImageFileType.From(request.ImageFileType),
            request.ImageSizeInBytes));

        return new LinkNewCoverToGenreCommandResult();
    }
}
