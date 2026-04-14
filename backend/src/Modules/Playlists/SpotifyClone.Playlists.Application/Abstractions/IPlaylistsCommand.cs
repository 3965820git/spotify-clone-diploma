using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Playlists.Application.Abstractions;

public interface IPlaylistsPersistentCommand
    : IPersistentCommand;

public interface IPlaylistsPersistentCommand<TResponse>
    : IPersistentCommand<TResponse>;
