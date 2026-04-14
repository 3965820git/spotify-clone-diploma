using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.Unverify;

public sealed class UnverifyArtistCommandValidator
    : AbstractValidator<UnverifyArtistCommand>
{
    public UnverifyArtistCommandValidator()
        => RuleFor(x => x.ArtistId)
            .NotNull().WithMessage("Artist ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Artist ID is required.");
}
