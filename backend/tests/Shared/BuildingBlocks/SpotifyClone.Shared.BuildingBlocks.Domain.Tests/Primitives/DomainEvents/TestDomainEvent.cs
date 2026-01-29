using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Shared.BuildingBlocks.Domain.Tests.Primitives.DomainEvents;

internal sealed record TestDomainEvent(int Value) : DomainEvent;
