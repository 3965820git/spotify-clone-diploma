using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.RescheduleRelease;

public sealed class RescheduleAlbumReleaseCommandValidator
    : AbstractValidator<RescheduleAlbumReleaseCommand>
{
    public RescheduleAlbumReleaseCommandValidator()
    {
        RuleFor(x => x.AlbumId)
            .NotNull().WithMessage("Album ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Album ID is required.");

        RuleFor(x => x.ReleaseDate)
            .NotNull().WithMessage("Release date is required.");
    }
}
