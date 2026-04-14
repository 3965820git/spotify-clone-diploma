using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.UnpublishAlbum;

public sealed class UnpublishAlbumCommandValidator
    : AbstractValidator<UnpublishAlbumCommand>
{
    public UnpublishAlbumCommandValidator()
        => RuleFor(x => x.AlbumId)
            .NotNull().WithMessage("Album ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Album ID is required.");
}
