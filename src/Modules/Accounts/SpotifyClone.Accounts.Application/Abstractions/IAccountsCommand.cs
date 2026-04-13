using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Accounts.Application.Abstractions;

public interface IAccountsPersistentCommand
    : IPersistentCommand;

public interface IAccountsPersistentCommand<TResponse>
    : IPersistentCommand<TResponse>;
