using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.Verify;

public sealed class VerifyArtistCommandValidator
    : AbstractValidator<VerifyArtistCommand>
{
    public VerifyArtistCommandValidator()
        => RuleFor(x => x.ArtistId)
            .NotNull().WithMessage("Artist ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Artist ID is required.");
}
