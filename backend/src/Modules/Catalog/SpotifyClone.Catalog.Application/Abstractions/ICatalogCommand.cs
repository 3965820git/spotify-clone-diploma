using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Catalog.Application.Abstractions;

public interface ICatalogPersistentCommand
    : IPersistentCommand;

public interface ICatalogPersistentCommand<TResponse>
    : IPersistentCommand<TResponse>;
