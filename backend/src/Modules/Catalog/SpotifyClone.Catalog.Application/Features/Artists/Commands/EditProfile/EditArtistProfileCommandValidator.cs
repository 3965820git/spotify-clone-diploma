using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.EditProfile;

public sealed class EditArtistProfileCommandValidator
    : AbstractValidator<EditArtistProfileCommand>
{
    public EditArtistProfileCommandValidator()
    {
        RuleFor(x => x.ArtistId)
            .NotNull().WithMessage("Artist ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Artist ID is required.");

        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is required.")
            .NotEmpty().WithMessage("Name is required.");
    }
}
