using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.Delete;

public sealed class DeleteAlbumCommandValidator
    : AbstractValidator<DeleteAlbumCommand>
{
    public DeleteAlbumCommandValidator()
        => RuleFor(x => x.AlbumId)
            .NotNull().WithMessage("Album ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Album ID is required.");
}
