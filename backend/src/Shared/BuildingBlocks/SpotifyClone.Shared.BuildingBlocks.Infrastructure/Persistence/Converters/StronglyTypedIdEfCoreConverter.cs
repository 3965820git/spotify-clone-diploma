using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Shared.BuildingBlocks.Infrastructure.Persistence.Converters;

public sealed class StronglyTypedIdEfCoreConverter<TId, TValue> : ValueConverter<TId, TValue>
    where TId : StronglyTypedId<TValue>
    where TValue : notnull
{
    public StronglyTypedIdEfCoreConverter(
        Expression<Func<TValue, TId>> fromProvider)
        : base(id => id.Value, fromProvider)
    {
    }
}
