using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.UpdateMainArtists;

public sealed class UpdateAlbumMainArtistsCommandValidator
    : AbstractValidator<UpdateAlbumMainArtistsCommand>
{
    public UpdateAlbumMainArtistsCommandValidator()
    {
        RuleFor(x => x.AlbumId)
            .NotNull().WithMessage("Album ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Album ID is required.");

        RuleFor(x => x.MainArtists)
            .NotEmpty().WithMessage("Main artists are required.");
    }
}
