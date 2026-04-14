using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.CorrectTitle;

public sealed class CorrectAlbumTitleCommandValidator
    : AbstractValidator<CorrectAlbumTitleCommand>
{
    public CorrectAlbumTitleCommandValidator()
    {
        RuleFor(x => x.AlbumId)
            .NotNull().WithMessage("Album ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Album ID is required.");

        RuleFor(x => x.Title)
            .NotNull().WithMessage("Title is required.")
            .NotEmpty().WithMessage("Album ID is required.");
    }
}
