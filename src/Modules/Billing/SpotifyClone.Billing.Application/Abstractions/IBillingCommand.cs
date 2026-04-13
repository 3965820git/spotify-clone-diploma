using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Billing.Application.Abstractions;

public interface IBillingPersistentCommand
    : IPersistentCommand;

public interface IBillingPersistentCommand<TResponse>
    : IPersistentCommand<TResponse>;
