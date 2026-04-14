using SpotifyClone.Playlists.Application.Abstractions.Repositories;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions;

namespace SpotifyClone.Playlists.Application.Abstractions;

public interface IPlaylistsUnitOfWork : IUnitOfWork
{
    IPlaylistRepository Playlists { get; }
    ITrackReferenceRepository TrackReferences { get; }
    IUserReferenceRepository UserReferences { get; }
    IOutboxRepository OutboxMessages { get; }
}
