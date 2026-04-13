using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.Ban;

public sealed class DeleteArtistCommandValidator
    : AbstractValidator<BanArtistCommand>
{
    public DeleteArtistCommandValidator()
        => RuleFor(x => x.ArtistId)
            .NotNull().WithMessage("Artist ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Artist ID is required.");
}
