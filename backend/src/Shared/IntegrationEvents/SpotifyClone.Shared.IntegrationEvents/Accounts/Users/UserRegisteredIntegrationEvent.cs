using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;

namespace SpotifyClone.Shared.IntegrationEvents.Accounts.Users;

public sealed record UserRegisteredIntegrationEvent(
    Guid UserId,
    string Name,
    Guid? AvatarImageId)
    : IntegrationEvent;
