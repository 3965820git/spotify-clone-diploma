using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Genres.Commands.LinkNewCover;

public sealed class LinkNewCoverToGenreCommandValidator
    : AbstractValidator<LinkNewCoverToGenreCommand>
{
    public LinkNewCoverToGenreCommandValidator()
    {
        RuleFor(x => x.GenreId)
            .NotNull().WithMessage("Genre ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Genre ID is required.");

        RuleFor(x => x.ImageWidth)
            .NotNull().WithMessage("Image width is required.");

        RuleFor(x => x.ImageHeight)
            .NotNull().WithMessage("Image height is required.");

        RuleFor(x => x.ImageFileType)
            .NotNull().WithMessage("Image file type is required.");

        RuleFor(x => x.ImageSizeInBytes)
            .NotNull().WithMessage("Image size in bytes is required.");
    }
}
