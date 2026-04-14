using System.Linq.Expressions;

namespace SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Services;

public interface IBackgroundJobService
{
    void Enqueue<T>(Expression<Func<T, Task>> methodCall);
}
