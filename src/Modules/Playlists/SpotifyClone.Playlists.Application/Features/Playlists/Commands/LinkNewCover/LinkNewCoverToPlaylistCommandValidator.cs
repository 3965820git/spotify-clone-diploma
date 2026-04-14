using FluentValidation;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.LinkNewCover;

public sealed class LinkNewCoverToPlaylistCommandValidator
    : AbstractValidator<LinkNewCoverToPlaylistCommand>
{
    public LinkNewCoverToPlaylistCommandValidator()
    {
        RuleFor(x => x.PlaylistId)
            .NotNull().WithMessage("Playlist ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Playlist ID is required.");

        RuleFor(x => x.ImageWidth)
            .NotNull().WithMessage("Image width is required.");

        RuleFor(x => x.ImageHeight)
            .NotNull().WithMessage("Image height is required.");

        RuleFor(x => x.ImageFileType)
            .NotNull().WithMessage("Image file type is required.");

        RuleFor(x => x.ImageSizeInBytes)
            .NotNull().WithMessage("Image size in bytes is required.");
    }
}
