using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Billing.Application.Features.Subscriptions.Commands.CreateCheckoutSession;

public sealed record CreateCheckoutSessionCommand
    : ICommand<CreateCheckoutSessionCommandResult>;
