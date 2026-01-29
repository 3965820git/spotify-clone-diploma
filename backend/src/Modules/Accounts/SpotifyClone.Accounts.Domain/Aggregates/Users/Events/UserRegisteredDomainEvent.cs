using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Accounts.Domain.Aggregates.Users.Events;

public sealed record UserRegisteredDomainEvent(
    Guid UserId,
    string Email,
    string ConfirmationToken) : DomainEvent;
