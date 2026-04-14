using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.UnlinkBanner;

public sealed class UnlinkBannerFromArtistCommandValidator
    : AbstractValidator<UnlinkBannerFromArtistCommand>
{
    public UnlinkBannerFromArtistCommandValidator()
        => RuleFor(x => x.ArtistId)
            .NotNull().WithMessage("Artist ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Artist ID is required.");
}
