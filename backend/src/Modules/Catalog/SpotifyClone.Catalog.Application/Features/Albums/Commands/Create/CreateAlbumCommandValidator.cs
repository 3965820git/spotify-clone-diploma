using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.Create;

public sealed class CreateAlbumCommandValidator
    : AbstractValidator<CreateAlbumCommand>
{
    public CreateAlbumCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull().WithMessage("Title is required.")
            .NotEmpty().WithMessage("Title is required.");

        RuleFor(x => x.MainArtists)
            .NotNull().WithMessage("Main artists are required.")
            .NotEmpty().WithMessage("Main artists are required.");
    }
}
