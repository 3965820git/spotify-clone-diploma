using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.PublishAlbum;

public sealed class PublishAlbumCommandValidator
    : AbstractValidator<PublishAlbumCommand>
{
    public PublishAlbumCommandValidator()
    {
        RuleFor(x => x.AlbumId)
            .NotNull().WithMessage("Album ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Album ID is required.");

        RuleFor(x => x.ReleaseDate)
            .NotNull().WithMessage("Release date is required.");
    }
}
