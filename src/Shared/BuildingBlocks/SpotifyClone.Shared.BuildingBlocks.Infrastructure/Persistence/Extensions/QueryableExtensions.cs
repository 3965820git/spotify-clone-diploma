using Microsoft.EntityFrameworkCore;
using SpotifyClone.Shared.BuildingBlocks.Application.Pagination;

namespace SpotifyClone.Shared.BuildingBlocks.Infrastructure.Persistence.Extensions;

public static class QueryableExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> source,
        int page,
        int pageSize)
    {
        // 1. Спочатку рахуємо загальну кількість (виконує SELECT COUNT)
        int count = await source.CountAsync();

        // 2. Витягуємо тільки потрібну сторінку (виконує LIMIT / OFFSET)
        List<T> items = await source
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<T>(items, count, page, pageSize);
    }
}
