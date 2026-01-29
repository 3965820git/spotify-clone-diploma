using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Domain.Tests.Primitives.StronglyTypedIds;

namespace SpotifyClone.Shared.BuildingBlocks.Domain.Tests.Primitives.Entities;

internal sealed class OtherTestEntity(OtherTestId id, string name)
    : Entity<OtherTestId, Guid>(id)
{
    public string Name { get; set; } = name;
}
