using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.LinkNewBanner;

public sealed class LinkNewBannerToArtistCommandValidator
    : AbstractValidator<LinkNewBannerToArtistCommand>
{
    public LinkNewBannerToArtistCommandValidator()
    {
        RuleFor(x => x.ArtistId)
            .NotNull().WithMessage("Artist ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Artist ID is required.");

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
