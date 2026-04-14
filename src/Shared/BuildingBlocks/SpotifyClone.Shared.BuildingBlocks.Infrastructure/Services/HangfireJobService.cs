using System.Linq.Expressions;
using Hangfire;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Services;

namespace SpotifyClone.Shared.BuildingBlocks.Infrastructure.Services;

public class HangfireJobService(IBackgroundJobClient jobClient) : IBackgroundJobService
{
    public void Enqueue<T>(Expression<Func<T, Task>> methodCall)
        => jobClient.Enqueue(methodCall);
}
