namespace SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

public abstract class Entity<TId, TIdValue>
    : IEquatable<Entity<TId, TIdValue>>
    where TId : notnull, StronglyTypedId<TIdValue>
    where TIdValue : notnull
{
    public TId Id { get; private set; }

    protected Entity()
        => Id = default!;

    protected Entity(TId id)
        => Id = id ?? throw new ArgumentNullException(nameof(id));

    public static bool operator ==(Entity<TId, TIdValue>? left, Entity<TId, TIdValue>? right)
        => left is not null && left.Equals(right);

    public static bool operator !=(Entity<TId, TIdValue>? left, Entity<TId, TIdValue>? right)
        => !(left == right);

    public bool Equals(Entity<TId, TIdValue>? other)
        => other is not null && Id!.Equals(other.Id);

    public override bool Equals(object? obj)
        => obj is not null
        && obj.GetType() == GetType()
        && obj is Entity<TId, TIdValue> entity
        && Id!.Equals(entity.Id);

    public override int GetHashCode()
        => Id!.GetHashCode();
}
