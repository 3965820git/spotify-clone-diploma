namespace SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

public abstract record StronglyTypedId<TValue> : ValueObject
    where TValue : notnull
{
    public TValue Value { get; }

    protected StronglyTypedId(TValue value)
    {
        ArgumentNullException.ThrowIfNull(value);
        Value = value;
    }

    public override string ToString() => Value.ToString()!;
}
