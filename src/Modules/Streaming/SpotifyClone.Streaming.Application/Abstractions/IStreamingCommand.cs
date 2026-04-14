using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Streaming.Application.Abstractions;

public interface IStreamingPersistentCommand
    : IPersistentCommand;

public interface IStreamingPersistentCommand<TResponse>
    : IPersistentCommand<TResponse>;
