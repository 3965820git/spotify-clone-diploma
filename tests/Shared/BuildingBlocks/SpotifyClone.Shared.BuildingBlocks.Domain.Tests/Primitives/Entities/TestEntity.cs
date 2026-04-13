using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Domain.Tests.Primitives.StronglyTypedIds;

namespace SpotifyClone.Shared.BuildingBlocks.Domain.Tests.Primitives.Entities;

internal sealed class TestEntity(TestId id, string name)
    : Entity<TestId, Guid>(id)
{
    public string Name { get; set; } = name;
}
