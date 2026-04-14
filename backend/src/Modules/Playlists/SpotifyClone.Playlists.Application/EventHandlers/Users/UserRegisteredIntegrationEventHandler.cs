using MediatR;
using Microsoft.Extensions.Logging;
using SpotifyClone.Playlists.Application.Abstractions;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists.Enums;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists.ValueObjects;
using SpotifyClone.Shared.IntegrationEvents.Accounts.Users;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Playlists.Application.EventHandlers.Users;

internal sealed class UserRegisteredIntegrationEventHandler(
    IPlaylistsUnitOfWork unit,
    ILogger<UserRegisteredIntegrationEventHandler> logger)
    : INotificationHandler<UserRegisteredIntegrationEvent>
{
    private readonly IPlaylistsUnitOfWork _unit = unit;
    private readonly ILogger<UserRegisteredIntegrationEventHandler> _logger = logger;

    public async Task Handle(
        UserRegisteredIntegrationEvent notification,
        CancellationToken cancellationToken)
    {
        await SaveUserReferenceAsync(notification, cancellationToken);
        await InitializeSystemPlaylistsAsync(notification, cancellationToken);
    }

    private async Task SaveUserReferenceAsync(
        UserRegisteredIntegrationEvent notification,
        CancellationToken cancellationToken = default)
    {
        if (await _unit.UserReferences.ExistsAsync(notification.UserId, cancellationToken))
        {
            _logger.LogError(
                "User {UserId} already exists in the Playlists module.",
                notification.UserId);
            throw new InvalidOperationException(
                $"User {notification.UserId} already exists in the Playlists module.");
        }

        await _unit.UserReferences.AddAsync(
            notification.UserId,
            notification.Name,
            notification.AvatarImageId,
            cancellationToken);

        await _unit.CommitAsync(cancellationToken);
    }

    private async Task InitializeSystemPlaylistsAsync(
        UserRegisteredIntegrationEvent notification,
        CancellationToken cancellationToken = default)
    {
        if (!await _unit.UserReferences.ExistsAsync(notification.UserId, cancellationToken))
        {
            _logger.LogError(
                "User {UserId} was not found in the Playlists module.",
                notification.UserId);
            throw new InvalidOperationException(
                $"User {notification.UserId} was not found in the Playlist module.");
        }

        var playlist = Playlist.CreateSystemPlaylist(
            PlaylistId.New(),
            UserId.From(notification.UserId),
            "Liked Songs",
            null,
            PlaylistType.LikedTracks);

        await _unit.Playlists.AddAsync(playlist, cancellationToken);

        await _unit.CommitAsync(cancellationToken);
    }
}
