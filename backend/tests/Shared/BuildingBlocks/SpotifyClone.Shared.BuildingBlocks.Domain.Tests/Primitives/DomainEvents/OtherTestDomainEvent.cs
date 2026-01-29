using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Shared.BuildingBlocks.Domain.Tests.Primitives.DomainEvents;

internal sealed record OtherTestDomainEvent(int Value) : DomainEvent;
