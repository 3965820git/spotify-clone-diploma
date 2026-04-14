using SpotifyClone.Playlists.Application.Abstractions;
using SpotifyClone.Playlists.Application.Errors;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.Create;

internal sealed class CreatePlaylistCommandHandler(
    IPlaylistsUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<CreatePlaylistCommand, CreatePlaylistCommandResult>
{
    private readonly IPlaylistsUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<CreatePlaylistCommandResult>> Handle(
        CreatePlaylistCommand request,
        CancellationToken cancellationToken)
    {
        if (!_currentUser.IsAuthenticated ||
            !await _unit.UserReferences.ExistsAsync(_currentUser.Id, cancellationToken))
        {
            return Result.Failure<CreatePlaylistCommandResult>(PlaylistErrors.InvalidOwner);
        }

        var ownerId = UserId.From(_currentUser.Id);
        IEnumerable<Playlist> playlists = await _unit.Playlists.GetAllByOwnerAsync(
                ownerId,
                cancellationToken);

        var playlist = Playlist.Create(
            PlaylistId.New(),
            ownerId,
            playlists.Count());

        await _unit.Playlists.AddAsync(playlist, cancellationToken);

        return new CreatePlaylistCommandResult(playlist.Id.Value);
    }
}
