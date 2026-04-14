using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.Unban;

public sealed class UnbanArtistCommandValidator
    : AbstractValidator<UnbanArtistCommand>
{
    public UnbanArtistCommandValidator()
        => RuleFor(x => x.ArtistId)
            .NotNull().WithMessage("Artist ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Artist ID is required.");
}
