using SpotifyClone.Accounts.Application.Abstractions;

namespace SpotifyClone.Accounts.Application.Features.Accounts.Commands.LinkNewAvatar;

public sealed record LinkNewAvatarToUserCommand(
    Guid UserId,
    Guid ImageId,
    int ImageWidth,
    int ImageHeight,
    string ImageFileType,
    long ImageSizeInBytes)
    : IAccountsPersistentCommand<LinkNewAvatarToUserCommandResult>;
