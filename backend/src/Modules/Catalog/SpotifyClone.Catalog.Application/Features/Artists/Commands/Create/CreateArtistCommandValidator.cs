using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.Create;

public sealed class CreateArtistCommandValidator
    : AbstractValidator<CreateArtistCommand>
{
    public CreateArtistCommandValidator()
        => RuleFor(x => x.Name)
            .NotNull().WithMessage("Artist ID is required.")
            .NotEmpty().WithMessage("Artist ID is required.");
}
